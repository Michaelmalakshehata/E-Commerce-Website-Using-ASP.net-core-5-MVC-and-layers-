using E_Commerce_Website.BL.IRepositories;
using E_Commerce_Website.BL.ViewModels;
using E_Commerce_Website.DAL;
using E_Commerce_Website.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.Repositories
{
    public class ServiceComments : IServiceComments
    {
        private readonly EntityContext context;
        public ServiceComments(EntityContext context)
        {
            this.context = context;

        }

        public async Task<Comments> AddComment(CommentViewModel comment)
        {
            try
            {
                if (comment is not null)
                {
                    var userId = await context.Users.Where(o => o.UserName.Equals(comment.UserName)).Select(o => o.Id).FirstOrDefaultAsync();
                    Comments comments = new Comments()
                    {
                        MenuId = comment.ProductId,
                        UserName = comment.UserName,
                        Comment = comment.Comments,
                        Rating = comment.Rating,
                        UserId = userId
                    };

                    var result = await context.Comments.AddAsync(comments);
                    await context.SaveChangesAsync();
                }
                return new Comments();
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteComment(int Id)
        {
            try
            {
                if (Id > 0)
                {
                    Comments comment = await context.Comments.Where(o => o.Id == Id).FirstOrDefaultAsync();
                    if (comment is not null)
                    {
                        context.Comments.Remove(comment);
                        await context.SaveChangesAsync();
                        return "deleted";
                    }
                }
                return string.Empty;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CommentViewModel>> GetAllCommentsOfProdect(int productId)
        {
            try
            {
                if (productId > 0)
                {
                    List<CommentViewModel> comments = new List<CommentViewModel>();
                    List<Comments> commentList = await context.Comments.Where(o => o.MenuId == productId).ToListAsync();
                    foreach (var cmnt in commentList)
                    {
                        comments.Add(new CommentViewModel
                        {
                            ProductId = cmnt.MenuId,
                            UserName = cmnt.UserName,
                            Comments = cmnt.Comment,
                            Date = cmnt.Created,
                            Rating=cmnt.Rating,
                            Id=cmnt.Id
                        });
                    }
                    return comments;
                }
                return new List<CommentViewModel>();
            }
            catch
            {
                throw;
            }
        }
    }
}
