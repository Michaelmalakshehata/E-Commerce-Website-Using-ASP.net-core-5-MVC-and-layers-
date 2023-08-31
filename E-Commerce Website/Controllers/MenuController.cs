using E_Commerce_Website.BL.IRepositories;
using E_Commerce_Website.BL.Pagination;
using E_Commerce_Website.BL.Repositories;
using E_Commerce_Website.BL.Search;
using E_Commerce_Website.BL.ViewModels;
using E_Commerce_Website.DAL.Constant;
using E_Commerce_Website.DAL.Models;
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
        public async Task<IActionResult> Index(string search,int pg=1)
        {

            var productlist = await serviceproduct.GetallProducts();
            if (search is not null)
            {
                var searchData = Search<Menus>.SearchByName(pg, search, productlist);
                this.ViewBag.Pager = searchData.Item2;
                return View(searchData.Item1);
            }
            var data = Pagination<Menus>.GetPaginationData(pg, productlist);
            this.ViewBag.Pager = data.Item2;
            return View(data.Item1);
        }

        #endregion


        #region all deleted product
        [HttpGet]
        public async Task<IActionResult> Deletedproduct(string search,int pg=1)
        {
            var productlistdeleted = await serviceproduct.GetalldeletedProducts();
            if (search is not null)
            {
                var searchData = Search<Menus>.SearchByNameDelete(pg, search, productlistdeleted);
                this.ViewBag.pager = searchData.Item2;
                return View(searchData.Item1);
            }
            var data = Pagination<Menus>.GetPaginationData(pg, productlistdeleted);
            this.ViewBag.pager = data.Item2;
            return View(data.Item1);
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
                var name = User.Identity.Name;
                var result = await serviceproduct.add(productViewModel,name);
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
                    var name = User.Identity.Name;
                    serviceproduct.Update(productUpdateViewModel,name);
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
            var name = User.Identity.Name;
            await serviceproduct.Delete(Id,name);
            return RedirectToAction("Index");
        }

        #endregion


        #region restore product

        [HttpGet]
        public async Task<IActionResult> RestoreProduct(int Id)
        {
            var name = User.Identity.Name;
            await serviceproduct.RestoreProduct(Id,name);
            return RedirectToAction("Deletedproduct");
        }

        #endregion
    }
}
