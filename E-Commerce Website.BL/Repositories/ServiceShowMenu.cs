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
    public class ServiceShowMenu : IServiceShowMenu
    {
        private readonly EntityContext context;
        private readonly IGenericRepository<Menus> genericRepository;
        public ServiceShowMenu(IGenericRepository<Menus> genericRepository, EntityContext context)
        {
            this.genericRepository = genericRepository;
            this.context = context;
        }
        public async Task<List<CartViewModel>> GetlastMenus()
        {
            List<Menus> list = await context.Menus.Where(e => e.IsDeleted == false).Include(e => e.Categories).Take(9).ToListAsync();
            if (list != null)
            {
                List<CartViewModel> cart = new List<CartViewModel>();

                foreach (var item in list)
                {
                    cart.Add(new CartViewModel
                    {
                        imgpath = item.imgpath,
                        Price = item.Price,
                        Ordername = item.Name,
                        MenuId = item.Id,
                        CategoryId = item.CategoryId,
                        CategoryName = item.Categories.Name
                    });
                }
                return cart;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<CartViewModel>> Takefristthreemenu()
        {
            List<Menus> list =await context.Menus.Where(e => e.IsDeleted == false).Take(3).ToListAsync();
            if (list != null)
            {
                List<CartViewModel> cart = new List<CartViewModel>();

                foreach (var item in list)
                {
                    cart.Add(new CartViewModel
                    {
                        imgpath = item.imgpath,
                        Price = item.Price,
                        Ordername = item.Name,
                        MenuId = item.Id,
                        CategoryId = item.CategoryId,
                        CategoryName = item.Categories.Name
                    });
                }
                return cart;
            }
            else
            {
                return null;
            }

        }
        public async Task<Menus> GetByid(int id)
        {
            if (id > 0)
            {
                var result = await genericRepository.GetById(id);
                if (result != null)
                {
                  
                    return result;
                }

            }
            return null;
        }

        public async Task<List<CartViewModel>> GetMenuByCategory(int id)
        {
            if (id != 0)
            {
                List<Menus> menus = await context.Menus.Where(e => e.IsDeleted == false).Where(s => s.CategoryId == id).ToListAsync();
                if (menus != null)
                {
                    List<CartViewModel> cart = new List<CartViewModel>();

                    foreach (var item in menus)
                    {
                        cart.Add(new CartViewModel
                        {
                            imgpath = item.imgpath,
                            Price = item.Price,
                            Ordername = item.Name,
                            MenuId = item.Id,
                            CategoryId = item.CategoryId
                        });
                    }
                    return cart;
                }
            }
            return null;
        }

        public async Task<List<CartViewModel>> SearchMenu(SearchViewModel searchViewModel)
        {
            if (searchViewModel.name != null)
            {
                List<Menus> list = await context.Menus.Where(o => o.IsDeleted == false).Where(o => o.Name.Contains(searchViewModel.name)).Include(e => e.Categories).ToListAsync();
                if (list != null)
                {
                    List<CartViewModel> cart = new List<CartViewModel>();

                    foreach (var item in list)
                    {
                        cart.Add(new CartViewModel
                        {
                            imgpath = item.imgpath,
                            Price = item.Price,
                            Ordername = item.Name,
                            MenuId = item.Id,
                            CategoryId = item.CategoryId,
                            CategoryName = item.Categories.Name
                        });
                    }
                    return cart;
                }
            }
            return null;

        }

        public async Task<List<CartViewModel>> TakeFristProduct()
        {
            List<Menus> list = await context.Menus.Where(e => e.IsDeleted == false).Take(1).ToListAsync();
            if (list != null)
            {
                List<CartViewModel> cart = new List<CartViewModel>();

                foreach (var item in list)
                {
                    cart.Add(new CartViewModel
                    {
                        imgpath = item.imgpath,
                        Price = item.Price,
                        Ordername = item.Name,
                        MenuId = item.Id,
                        CategoryId = item.CategoryId,
                        CategoryName = item.Categories.Name
                    });
                }
                return cart;
            }
            else
            {
                return null;
            }

        }
    }
}
