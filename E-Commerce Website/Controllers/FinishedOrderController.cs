using E_Commerce_Website.BL.IRepositories;
using E_Commerce_Website.DAL.Constant;
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

        public async Task<IActionResult> Index()
        {
            var listorders = await serviceFinishedOrder.GetAllFinishedOrder();
            return View(listorders);
        }
        [HttpGet]
        public async Task<IActionResult> AddFinishedOrder(int Id)
        {
            await serviceFinishedOrder.AddFinishedOrder(Id);
            return RedirectToAction("Index", "RequestOrder");
        }
    }
}
