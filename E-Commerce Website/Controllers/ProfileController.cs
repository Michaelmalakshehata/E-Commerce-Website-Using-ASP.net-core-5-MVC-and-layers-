using E_Commerce_Website.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce_Website.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            string name = User.Identity.Name;
            ApplicationUser user = await userManager.FindByNameAsync(name);
            if (user is not null)
            {
                return View(user);
            }

            return View();
        }
    }
}
