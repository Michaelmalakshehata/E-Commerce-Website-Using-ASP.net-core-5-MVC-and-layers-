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
        public async Task<List<CartViewModel>> GetAllMenus()
        {
            try
            {
                List<Menus> list = await context.Menus.Where(e => e.IsDeleted == false).Include(e => e.Categories).ToListAsync();
                if (list is not null)
                {
                    List<CartViewModel> cart = new List<CartViewModel>();

                    foreach (var item in list)
                    {
                        cart.Add(new CartViewModel
                        {
                            imgpath = item.imgpath,
                            Price = item.Price,
                            Discount = item.Discount,
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
                    return new List<CartViewModel>();
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<CartViewModel>> Takefristthreemenu()
        {
            try
            {
                List<Menus> list = await context.Menus.Where(e => e.IsDeleted == false).Take(3).ToListAsync();
                if (list is not null)
                {
                    List<CartViewModel> cart = new List<CartViewModel>();

                    foreach (var item in list)
                    {
                        cart.Add(new CartViewModel
                        {
                            imgpath = item.imgpath,
                            Price = item.Price,
                            Discount = item.Discount,
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
                    return new List<CartViewModel>();
                }
            }
            catch
            {
                throw;
            }

        }
        public async Task<Menus> GetByid(int id)
        {
            try
            {
                if (id > 0)
                {
                    Menus result = await genericRepository.GetById(id);

                    if (result is not null)
                    {

                        return result;
                    }

                }
                return new Menus();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CartViewModel>> GetMenuByCategory(int id)
        {
            try
            {
                if (id != 0)
                {
                    List<Menus> menus = await context.Menus.Where(e => e.IsDeleted == false).Where(s => s.CategoryId == id).ToListAsync();
                    if (menus is not null)
                    {
                        List<CartViewModel> cart = new List<CartViewModel>();

                        foreach (var item in menus)
                        {
                            cart.Add(new CartViewModel
                            {
                                imgpath = item.imgpath,
                                Price = item.Price,
                                Discount = item.Discount,
                                Ordername = item.Name,
                                MenuId = item.Id,
                                CategoryId = item.CategoryId
                            });
                        }
                        return cart;
                    }
                }
                return new List<CartViewModel>();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CartViewModel>> SearchMenu(SearchViewModel searchViewModel)
        {
            try
            {
                if (searchViewModel is not null)
                {
                    List<Menus> list = await context.Menus.Where(o => o.IsDeleted == false).Where(o => o.Name.Contains(searchViewModel.name)).Include(e => e.Categories).ToListAsync();
                    if (list is not null)
                    {
                        List<CartViewModel> cart = new List<CartViewModel>();

                        foreach (var item in list)
                        {
                            cart.Add(new CartViewModel
                            {
                                imgpath = item.imgpath,
                                Price = item.Price,
                                Discount = item.Discount,
                                Ordername = item.Name,
                                MenuId = item.Id,
                                CategoryId = item.CategoryId,
                                CategoryName = item.Categories.Name
                            });
                        }
                        return cart;
                    }
                }
                return new List<CartViewModel>();
            }
            catch
            {
                throw;
            }

        }

        public async Task<List<CartViewModel>> TakeFristProduct()
        {
            try
            {
                List<Menus> list = await context.Menus.Where(e => e.IsDeleted == false).Take(1).ToListAsync();
                if (list is not null)
                {
                    List<CartViewModel> cart = new List<CartViewModel>();

                    foreach (var item in list)
                    {
                        cart.Add(new CartViewModel
                        {
                            imgpath = item.imgpath,
                            Price = item.Price,
                            Discount = item.Discount,
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
                    return new List<CartViewModel>();
                }
            }
            catch
            {
                throw;
            }

        }
    }
}
