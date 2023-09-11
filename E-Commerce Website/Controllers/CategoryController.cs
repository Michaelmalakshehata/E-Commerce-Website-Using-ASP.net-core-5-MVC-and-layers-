using E_Commerce_Website.BL.IRepositories;
using E_Commerce_Website.BL.Pagination;
using E_Commerce_Website.BL.Search;
using E_Commerce_Website.BL.ViewModels;
using E_Commerce_Website.DAL.Constant;
using E_Commerce_Website.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Website.Controllers
{
        [Authorize(Roles = Roles.adminrole)]
        public class CategoryController : Controller
        {
            private readonly IServiceCategory serviceCategory;
           public CategoryController(IServiceCategory serviceCategory)
            {
                this.serviceCategory = serviceCategory;
            }
            #region all categories
            public async Task<IActionResult> Index(string search, int pg=1)
            {
                List<Categories> Categorylist = await serviceCategory.GetallCategories();
                if(search is not null)
                {
                    var searchData = Search<Categories>.SearchByName(pg, search, Categorylist);
                    this.ViewBag.Pager = searchData.Item2;
                    return View(searchData.Item1);
                }
                var data = Pagination<Categories>.GetPaginationData(pg, Categorylist);
                this.ViewBag.Pager = data.Item2;
                return View(data.Item1);
            }

            #endregion


            #region  deleted categories
            [HttpGet]
            public async Task<IActionResult> DeletedCategory(string search,int pg=1)
            {
                var Categorylistdeleted = await serviceCategory.GetalldeletedCategories();
                if (search is not null)
                {
                    var searchData = Search<Categories>.SearchByNameDelete(pg, search, Categorylistdeleted);
                    this.ViewBag.pager = searchData.Item2;
                    return View(searchData.Item1);
                }
                var data = Pagination<Categories>.GetPaginationData(pg, Categorylistdeleted);
                this.ViewBag.pager = data.Item2;
                return View(data.Item1);
            }

            #endregion

            #region  create categories
            [HttpGet]
            public IActionResult CreateCategory()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> CreateCategory(CategoryViewModel categoryViewModel)
            {

                if (ModelState.IsValid)
                {
               var name = User.Identity.Name;
                var result = await serviceCategory.add(categoryViewModel,name);
                    return RedirectToAction("Index");
                }

                return View(categoryViewModel);
            }

            #endregion

            #region  updated categories

            [HttpGet]
            public async Task<IActionResult> UpdateCategory(int Id)
            {
                var result = await serviceCategory.GetByid(Id);
                return View(result);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> UpdateCategory(CategoryUpdateViewModel categoryUpdateViewModel)
            {
                if (ModelState.IsValid)
                {
                var name = User.Identity.Name;
                await serviceCategory.Update(categoryUpdateViewModel,name);
                    return RedirectToAction("Index");
                }
                return View(categoryUpdateViewModel);
            }

            #endregion

            #region  deleted categories

            [HttpGet]
            public async Task<IActionResult> DeleteCategory(int Id)
            {
                var name = User.Identity.Name;
                await serviceCategory.Delete(Id,name);
                return RedirectToAction("Index");
            }

            #endregion


            #region restore categories

            [HttpGet]
            public async Task<IActionResult> RestoreCategory(int Id)
            {
                var name = User.Identity.Name;
                await serviceCategory.RestoreCategory(Id,name);
                return RedirectToAction("DeletedCategory");
            }

            #endregion
        }
    
}
