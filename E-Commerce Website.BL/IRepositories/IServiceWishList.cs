using E_Commerce_Website.BL.ViewModels;
using E_Commerce_Website.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.IRepositories
{
    public interface IServiceWishList
    {
        Task<List<WishList>> GetAllUserWishList(string Name);
        Task<string> AddToWishList(int Id, string name);
        Task<string> DeleteWishListItem(int id, string name);
        Task<string> DeleteAllWishListItems(string name);
        Task<int> GetWishListCount(string name);

    }
}
