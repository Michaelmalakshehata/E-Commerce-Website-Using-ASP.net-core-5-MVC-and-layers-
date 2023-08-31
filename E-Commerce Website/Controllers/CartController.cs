using E_Commerce_Website.BL.IRepositories;
using E_Commerce_Website.BL.Pagination;
using E_Commerce_Website.BL.ViewModels;
using E_Commerce_Website.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce_Website.Controllers
{
    public class CartController : Controller
    {
        private readonly IServiceCart serviceCart;
        public CartController(IServiceCart serviceCart)
        {
            this.serviceCart = serviceCart;
        }
        public async Task<IActionResult> Index(int pg=1)
        {
            string name = User.Identity.Name;
            var carts = await serviceCart.GetAllUserCart(name);
            var data = Pagination<CartUpdateViewModel>.GetPaginationData(pg, carts);
            this.ViewBag.Pager = data.Item2;
            double totalprice = await serviceCart.totalprice(name);
            if (data.Item1 is not null && totalprice != 0)
            {
                ViewBag.price = totalprice;
                ViewBag.list = data.Item1;
                return View();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddToCart(CartViewModel cartViewModel)
        {
            if (ModelState.IsValid)
            {
                string name = User.Identity.Name;

                if (name is not null && cartViewModel is not null)
                {
                    var result = await serviceCart.AddToCart(cartViewModel, name);
                    if (result is not null)
                    {
                        return RedirectToAction("Index", "Cart");
                    }
                }
            }

            return RedirectToAction("Index", "ShowMenu");
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCart(CartUpdateViewModel cartUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                await serviceCart.UpdateCartItem(cartUpdateViewModel);
                return RedirectToAction("Index", "Cart");

            }
            return RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteCartItem(int Id)
        {
            string name = User.Identity.Name;
            if (Id != 0 && name is not null)
            {
                await serviceCart.DeleteCartItem(Id,name);
                return RedirectToAction("Index", "Cart");

            }
            return RedirectToAction("Index", "Cart");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteAllCartItems()
        {
            string name = User.Identity.Name;
            if (name is not null)
            {
                await serviceCart.DeleteAllCartItems(name);
                return RedirectToAction("Index", "Cart");

            }
            return RedirectToAction("Index", "Cart");
        }
    }
}
