using E_Commerce_Website.BL.IRepositories;
using E_Commerce_Website.BL.Pagination;
using E_Commerce_Website.DAL.Models;
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
        public async Task<IActionResult> Index(int pg=1)
        {
            string name = User.Identity.Name;
            if (name is not null)
            {
                var wishlist = await servicewishlist.GetAllUserWishList(name);
                var data = Pagination<WishList>.GetPaginationData(pg, wishlist);
                this.ViewBag.Pager = data.Item2;
                ViewBag.list = data.Item1;
                ViewBag.listcount = wishlist.Count;
                return View();
            }
            else
            {
                ViewBag.listcount = 0;
                return View();
            }

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddToWishList(int Id)
        {

            string name = User.Identity.Name;

            if (name is not null && Id != 0)
            {
                string result = await servicewishlist.AddToWishList(Id, name);
                if (result is not null)
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
            string name = User.Identity.Name;
            if (name is not null && Id != 0)
            {
                await servicewishlist.DeleteWishListItem(Id, name);
            }
            return RedirectToAction("Index", "WishList");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteAllWishListItems()
        {
            string name = User.Identity.Name;
            if (name is not null)
            {
                await servicewishlist.DeleteAllWishListItems(name);
            }
            return RedirectToAction("Index", "WishList");
        }
    }
}
