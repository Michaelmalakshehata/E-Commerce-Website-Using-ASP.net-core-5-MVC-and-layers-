using E_Commerce_Website.BL.ViewModels;
using E_Commerce_Website.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.IRepositories
{
    public interface IServiceProduct
    {
        Task<List<Menus>> GetallProducts();
        Task<List<Menus>> GetalldeletedProducts();
        Task<Menus> add(ProductViewModel productViewModel);
        Task<int> Delete(int id);
        Task<int> RestoreProduct(int id);
        Task<ProductUpdateViewModel> GetByid(int id);
       void Update(ProductUpdateViewModel productUpdateViewModel);
        Task<List<Menus>> GetProductByCategory(int id);
    }
}
