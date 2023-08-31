using E_Commerce_Website.BL.ViewModels;
using E_Commerce_Website.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.IRepositories
{
    public interface IServiceShowMenu
    {
        Task<List<CartViewModel>> GetAllMenus();
        Task<Menus> GetByid(int id);
        Task<List<CartViewModel>> GetMenuByCategory(int id);
        Task<List<CartViewModel>> Takefristthreemenu();
        Task<List<CartViewModel>> TakeFristProduct();

        Task<List<CartViewModel>> SearchMenu(SearchViewModel searchViewModel);
    }
}
