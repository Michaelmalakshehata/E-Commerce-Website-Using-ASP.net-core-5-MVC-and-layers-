using E_Commerce_Website.BL.ViewModels;
using E_Commerce_Website.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.IRepositories
{
    public interface IServiceComments
    {
        Task<List<CommentViewModel>> GetAllCommentsOfProdect(int productId);
        Task<string> DeleteComment(int Id);
        Task<Comments> AddComment(CommentViewModel comment);
    }
}
