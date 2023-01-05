using E_Commerce_Website.BL.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce_Website.Controllers
{
    public class WishListController : Controller
    {
        private readonly IServiceWishList servicewishlist;
        public WishListController(IServiceWishList servicewishlist)
        {
            this.servicewishlist = servicewishlist;
        }
        public async Task<IActionResult> Index()
        {
            string name = User.Identity.Name;
            if (name != null)
            {
                var wishlist = await servicewishlist.GetAllUserWishList(name);
                ViewBag.list = wishlist;
                ViewBag.listcount=wishlist.Count;   
                return View();
            }
            else {
                ViewBag.listcount = 0;
                return View();
            }

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddToWishList(int Id)
        {
           
                string name = User.Identity.Name;

                if (name != null && Id != 0)
                {
                    string result = await servicewishlist.AddToWishList(Id, name);
                    if (result != null)
                    {
                        return RedirectToAction("Index", "WishList");
                    }
                }           

            return RedirectToAction("Index", "ShowMenu");
        }
       

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteWishListItem(int Id)
        {
            if (Id != 0)
            {
                await servicewishlist.DeleteWishListItem(Id);
                return RedirectToAction("Index", "WishList");

            }
            return RedirectToAction("Index", "WishList");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteAllWishListItems()
        {
            string name = User.Identity.Name;
            if (name != null)
            {
                await servicewishlist.DeleteAllWishListItems(name);
                return RedirectToAction("Index", "WishList");

            }
            return RedirectToAction("Index", "WishList");
        }
    }
}
