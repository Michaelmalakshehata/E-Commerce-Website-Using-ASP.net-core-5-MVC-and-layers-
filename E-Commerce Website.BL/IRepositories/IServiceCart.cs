using E_Commerce_Website.BL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.IRepositories
{
    public interface IServiceCart
    {
        Task<List<CartUpdateViewModel>> GetAllUserCart(string Name);
        Task<string> AddToCart(CartViewModel cartViewModel, string name);
        Task<decimal> totalprice(string name);
        Task<string> UpdateCartItem(CartUpdateViewModel cartUpdateViewModel);
        Task<string> DeleteCartItem(int id, string name);
        Task<string> DeleteAllCartItems(string name);
        Task<int> GetCartCount(string name);
    }
}
