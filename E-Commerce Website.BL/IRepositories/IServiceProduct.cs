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
        Task<Menus> add(ProductViewModel productViewModel,string name);
        Task<int> Delete(int id,string name);
        Task<int> RestoreProduct(int id,string name);
        Task<ProductUpdateViewModel> GetByid(int id);
       Task Update(ProductUpdateViewModel productUpdateViewModel,string name);
        Task<List<Menus>> GetProductByCategory(int id);
    }
}
