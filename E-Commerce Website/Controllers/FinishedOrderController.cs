using E_Commerce_Website.BL.IRepositories;
using E_Commerce_Website.BL.Pagination;
using E_Commerce_Website.DAL.Constant;
using E_Commerce_Website.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce_Website.Controllers
{
    [Authorize(Roles = Roles.adminrole)]
    public class FinishedOrderController : Controller
    {
        private readonly IServiceFinishOrder serviceFinishedOrder;
        public FinishedOrderController(IServiceFinishOrder serviceFinishedOrder)
        {
            this.serviceFinishedOrder = serviceFinishedOrder;
        }

        public async Task<IActionResult> Index(int pg=1)
        {
            var listorders = await serviceFinishedOrder.GetAllFinishedOrder();
            var data = Pagination<FinishedOrders>.GetPaginationData(pg, listorders);
            this.ViewBag.Pager = data.Item2;
            return View(data.Item1);
        }
        [HttpGet]
        public async Task<IActionResult> AddFinishedOrder(int Id)
        {
            await serviceFinishedOrder.AddFinishedOrder(Id);
            return RedirectToAction("Index", "RequestOrder");
        }
    }
}
