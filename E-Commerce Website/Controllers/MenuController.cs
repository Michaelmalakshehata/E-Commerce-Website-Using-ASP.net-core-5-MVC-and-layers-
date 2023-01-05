using E_Commerce_Website.BL.IRepositories;
using E_Commerce_Website.BL.ViewModels;
using E_Commerce_Website.DAL.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace E_Commerce_Website.Controllers
{
    [Authorize(Roles = Roles.adminrole)]
    public class MenuController : Controller
    {
        private readonly IServiceProduct serviceproduct;
        private readonly IServiceCategory servicecategory;
        public MenuController(IServiceProduct serviceproduct, IServiceCategory servicecategory)
        {
            this.serviceproduct = serviceproduct;
            this.servicecategory = servicecategory;
        }
        #region all Products
        public async Task<IActionResult> Index()
        {

            var productlist = await serviceproduct.GetallProducts();
            if (productlist != null)
            {
                return View(productlist);
            }
            return View();
        }

        #endregion


        #region all deleted product
        [HttpGet]
        public async Task<IActionResult> Deletedproduct()
        {
            var productlistdeleted = await serviceproduct.GetalldeletedProducts();
            return View(productlistdeleted);
        }

        #endregion

        #region all create product
        [HttpGet]
        public async Task<IActionResult> CreateProducts()
        {
            var list = await servicecategory.GetallCategories();
            ViewBag.list = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProducts(ProductViewModel productViewModel)
        {

            if (ModelState.IsValid)
            {

                var result = await serviceproduct.add(productViewModel);
                return RedirectToAction("Index");
            }
            var list = await servicecategory.GetallCategories();
            ViewBag.list = list;
            return View(productViewModel);
        }

        #endregion

        #region all updated product

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int Id)
        {
            var result = await serviceproduct.GetByid(Id);
            var list = await servicecategory.GetallCategories();
            ViewBag.list = list;
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult UpdateProduct(ProductUpdateViewModel productUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                     serviceproduct.Update(productUpdateViewModel);
                    return RedirectToAction("Index");
                }
                catch (ArgumentNullException)
                {
                    ModelState.AddModelError("Name", "Updated Name Already exist in Menus or Restor it from Deleted Menus");
                }

            }
            var list =  servicecategory.GetallCategories();
            ViewBag.list = list;
            return View(productUpdateViewModel);
        }

        #endregion

        #region  deleted product

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            await serviceproduct.Delete(Id);
            return RedirectToAction("Index");
        }

        #endregion


        #region restore product

        [HttpGet]
        public async Task<IActionResult> RestoreProduct(int Id)
        {
            await serviceproduct.RestoreProduct(Id);
            return RedirectToAction("Deletedproduct");
        }

        #endregion
    }
}
