using E_Commerce_Website.BL.ViewModels;
using E_Commerce_Website.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.IRepositories
{
    public interface IServiceCategory
    {
        Task<List<Categories>> GetallCategories();
        Task<List<Categories>> GetalldeletedCategories();
        Task<Categories> add(CategoryViewModel categoryViewModel,string name);
        Task<int> Delete(int id,string name);
        Task<int> RestoreCategory(int id, string name);
        Task<CategoryUpdateViewModel> GetByid(int id);
        Task Update(CategoryUpdateViewModel categoryUpdateViewModel, string name);
    }
}
