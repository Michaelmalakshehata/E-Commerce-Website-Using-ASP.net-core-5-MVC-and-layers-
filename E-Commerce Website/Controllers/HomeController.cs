using E_Commerce_Website.BL.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce_Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceShowMenu serviceshowmenu;
        private readonly IServiceCategory serviceCategory;

        public HomeController(IServiceShowMenu serviceshowmenu, IServiceCategory _serviceCategory)
        {
            this.serviceCategory = _serviceCategory;
            this.serviceshowmenu = serviceshowmenu;
        }

        public async Task<IActionResult> Index()
        {
            var listcategory = await serviceCategory.GetallCategories();
            ViewBag.list = listcategory;
            ViewBag.listproduct = await serviceshowmenu.Takefristthreemenu();
            ViewBag.lastproduct=await serviceshowmenu.TakeFristProduct();
            return View();
        }
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMail()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}
