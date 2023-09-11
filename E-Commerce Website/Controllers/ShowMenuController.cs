using E_Commerce_Website.BL.IRepositories;
using E_Commerce_Website.BL.Pagination;
using E_Commerce_Website.BL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Website.Controllers
{
    public class ShowMenuController : Controller
    {
        private readonly IServiceComments serviceComments;
        private readonly IServiceShowMenu serviceshowmenu;
        private readonly IServiceCategory servicecategory;
        public ShowMenuController(IServiceShowMenu _serviceshowmenu, IServiceCategory servicecategory, IServiceComments serviceComments)
        {
            this.serviceshowmenu = _serviceshowmenu;
            this.servicecategory = servicecategory;
            this.serviceComments = serviceComments;
        }

        #region show all menu with category name
        public async Task<IActionResult> Index(int pg = 1)
        {

            var listmenus = await serviceshowmenu.GetAllMenus();
            var data = Pagination<CartViewModel>.GetPaginationData(pg, listmenus);
            this.ViewBag.Pager = data.Item2;
            if (data.Item1 is not null)
            {
                ViewBag.list = data.Item1;
            }
            return View();
        }

        #endregion

        #region show menu in detailes

        [HttpGet]
        public async Task<IActionResult> ShowDetailes(int Id,int pg=1)
        {
            ViewBag.menu = await serviceshowmenu.GetByid(Id);
            var ListOfComments = await serviceComments.GetAllCommentsOfProdect(Id);
            var data = Pagination<CommentViewModel>.GetPaginationData(pg, ListOfComments);
            this.ViewBag.Pager = data.Item2;
            ViewBag.listComment = data.Item1;
            if (ListOfComments.Count > 0)
            {
                var ratingSum = ListOfComments.Sum(d => d.Rating);
                ViewBag.RatingSum=ratingSum;
                var ratingCount = ListOfComments.Count();
                ViewBag.RatingCount=ratingCount;
            }
            else
            {
                ViewBag.RatingSum=0;
                ViewBag.RatingCount = 0;
            }

            return View();
        }
        #endregion

        #region show all menu in his category 

        [HttpGet]
        public async Task<IActionResult> ShowCategoryMenu(int Id, int pg = 1)
        {
            var menulist = await serviceshowmenu.GetMenuByCategory(Id);
            var data = Pagination<CartViewModel>.GetPaginationData(pg, menulist);
            this.ViewBag.pager = data.Item2;
            if (data.Item1 is not null)
            {
                ViewBag.list = data.Item1;
            }
            return View();
        }
        #endregion

        #region Search view
        [HttpGet]
        public IActionResult SearchMenu()
        {
            return View();
        }

        #endregion

        #region Search Result
        [HttpGet]
        public async Task<IActionResult> SearchResult(SearchViewModel searchViewModel, int pg = 1)
        {
            var list = await serviceshowmenu.SearchMenu(searchViewModel);
            var data = Pagination<CartViewModel>.GetPaginationData(pg, list);
            this.ViewBag.page = data.Item2;
            ViewBag.listmenu = data.Item1;
            return View();
        }
        #endregion
    }

}
