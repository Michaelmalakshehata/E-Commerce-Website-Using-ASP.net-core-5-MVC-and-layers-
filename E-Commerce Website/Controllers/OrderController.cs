using E_Commerce_Website.BL.IRepositories;
using E_Commerce_Website.BL.ViewModels;
using E_Commerce_Website.DAL.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce_Website.Controllers
{
    public class OrderController : Controller
    {
        private readonly IServiceOrder serviceOrder;
        private readonly IServiceFinishOrder serviceFinishedOrder;

        public OrderController(IServiceOrder serviceOrder, IServiceFinishOrder serviceFinishedOrder)
        {
            this.serviceOrder = serviceOrder;
            this.serviceFinishedOrder = serviceFinishedOrder;
        }
        [Authorize(Roles = Roles.userrole)]
        public async Task<IActionResult> AddOrder()
        {
            string name = User.Identity.Name;
            if (name != null)
            {
                var list = await serviceOrder.CheckoutOrder(name);
                ViewBag.totalprice = await serviceOrder.totalprice(name);
                return View(list);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrder(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                await serviceOrder.AddOrder(orderViewModel);
                return RedirectToAction("ShowOrders");
            }
            return View(orderViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ShowOrders()
        {
            string name = User.Identity.Name;
            ViewBag.list = await serviceOrder.Order(name);
            var listorders = await serviceFinishedOrder.GetAllFinishedOrderByName(name);
            return View(listorders);
        }
    }
}
