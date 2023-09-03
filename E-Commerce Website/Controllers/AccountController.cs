using E_Commerce_Website.BL.IRepositories;
using E_Commerce_Website.BL.Repositories.EmailService;
using E_Commerce_Website.BL.ViewModels;
using E_Commerce_Website.DAL.Constant;
using E_Commerce_Website.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace E_Commerce_Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender _emailSender;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._emailSender = emailSender;
        }
        #region Registration (Sign Up) 
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterViewModel modelvm)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    var user = new ApplicationUser()
                    {

                        UserName = modelvm.Username,
                        Email = modelvm.Email,
                        Address = modelvm.Address
                    };

                    var result = await userManager.CreateAsync(user, modelvm.Password);

                    if (result.Succeeded)
                    {
                        var token=await userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink=Url.Action(nameof(ConfirmEmail),"Account",new {token,email=user.Email},Request.Scheme);
                        var message = new Message(new string[]
                        {
                            user.Email
                        }, "Confirmation email link", confirmationLink, null);
                        await _emailSender.SendEmailAsync(message);
                        await userManager.AddToRoleAsync(user, Roles.userrole);
                        return RedirectToAction(nameof(SuccessRegistration));
                        
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }


                }

                return View(modelvm);

            }
            catch (Exception)
            {
                return View(modelvm);
            }
        }
        #endregion


        #region EmailConfirm (Sign Up) 
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token,string email)
        {
            var user=await userManager.FindByEmailAsync(email);
            if(user == null)
                return View("Error");
            var result = await userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SuccessRegistration()
        {
            return View();
        }
        #endregion


        #region Login (Sign In)

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel modelvm)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByNameAsync(modelvm.Username);
                    if (user is not null)
                    {
                        if(! await userManager.IsEmailConfirmedAsync(user))
                        {
                            ModelState.AddModelError("", "You Must Have Confirmed Email Login");
                            return View();
                        }
                        else
                        {
                            bool found = await userManager.CheckPasswordAsync(user, modelvm.Password);
                            if (found)
                            {
                                await signInManager.SignInAsync(user, modelvm.RememberMe);
                                return RedirectToAction("Index", "Home");

                            }
                        }
                       
                    }
                    ModelState.AddModelError("", "Invalid Email or Password");

                }

                return View(modelvm);

            }
            catch (Exception)
            {
                return View(modelvm);
            }
        }

        #endregion


        #region LogOff (Sign Out)

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


        #endregion


        #region Forgot and Reset Password
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(forgotPasswordModel);

            var user = await userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);

            var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);
            await _emailSender.SendEmailAsync(message);

            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {

            var model = new ResetPasswordModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordModel);

            var user = await userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
                RedirectToAction(nameof(ResetPasswordConfirmation));

            var resetPassResult = await userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return View();
            }

            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        #endregion
    }
}
