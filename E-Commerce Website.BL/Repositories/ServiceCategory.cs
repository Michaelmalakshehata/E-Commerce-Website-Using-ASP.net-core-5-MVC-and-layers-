using E_Commerce_Website.BL.IRepositories;
using E_Commerce_Website.BL.Pagination;
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
    public class ServiceCategory : IServiceCategory
    {
        private readonly EntityContext context;
        private readonly IGenericRepository<Categories> genericRepository;
        public ServiceCategory(IGenericRepository<Categories> genericRepository, EntityContext context)
        {
            this.genericRepository = genericRepository;
            this.context = context;
        }

        public async Task<Categories> add(CategoryViewModel categoryViewModel, string name)
        {
            try
            {
                if (categoryViewModel is not null && name is not null)
                {
                    Categories categories = new Categories()
                    {
                        Name = categoryViewModel.Name,
                        CreateUserName=name
                    };
                    return await genericRepository.add(categories);
                   
                }
                return new Categories();
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> Delete(int id, string name)
        {
            try
            {
                bool findProductInCategory=await context.Menus.Where(o=>o.IsDeleted==false&&o.CategoryId==id).AnyAsync();
                if (id > 0 && name is not null && findProductInCategory is false)
                {
                    return await genericRepository.delete(id,name);
                }
                return 0;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Categories>> GetallCategories()
        {
            try
            {
                var list = await genericRepository.Getall();              
                if (list is not null)
                {
                    return list;
                }
                else
                {
                    return new List<Categories>();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Categories>> GetalldeletedCategories()
        {
            try
            {
                List<Categories> list = await genericRepository.GetallSoftDeleted();
                if (list is not null)
                {
                    return list;
                }
                else
                {
                    return new List<Categories>();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<CategoryUpdateViewModel> GetByid(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await genericRepository.GetById(id);
                    if (result is not null)
                    {
                        CategoryUpdateViewModel categoryUpdateViewModel = new CategoryUpdateViewModel()
                        {
                            Name = result.Name,
                            Id = result.Id
                        };
                        return categoryUpdateViewModel;
                    }

                }
                return new CategoryUpdateViewModel();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> RestoreCategory(int id,string name)
        {
            try
            {
                if (id > 0 && name is not null)
                {
                    return await genericRepository.Restordeleted(id,name);                  
                }
                return 0;
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(CategoryUpdateViewModel categoryUpdateViewModel,string name)
        {
            try
            {
                if (categoryUpdateViewModel is not null &&name is not null)
                {

                  Categories oldcategory=await genericRepository.GetById(categoryUpdateViewModel.Id);
                    if (oldcategory is not null)
                    {
                        oldcategory.Name = categoryUpdateViewModel.Name;
                        await genericRepository.update(oldcategory, name);
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
