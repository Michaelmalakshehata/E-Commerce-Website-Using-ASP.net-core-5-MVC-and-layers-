using E_Commerce_Website.BL.IRepositories;
using E_Commerce_Website.BL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce_Website.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly IServiceComments serviceComments;
        public CommentsController(IServiceComments serviceComments)
        {
            this.serviceComments = serviceComments;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CommentViewModel comment)
        {
            if (ModelState.IsValid)
            {
                var name = User.Identity.Name;
                comment.UserName = name;
                await serviceComments.AddComment(comment);
            }
            return RedirectToAction("ShowDetailes", "ShowMenu", new { Id = comment.ProductId });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int commentId,int productID)
        {
            if (commentId > 0)
            {
                await serviceComments.DeleteComment(commentId);
            }
            return RedirectToAction("ShowDetailes", "ShowMenu", new { Id = productID});
        }
    }
}
