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
    public class ServiceCart : IServiceCart
    {
        private readonly EntityContext context;
        public ServiceCart(EntityContext context)
        {
            this.context = context;
        }

        public async Task<string> AddToCart(CartViewModel cartViewModel, string Name)
        {
            try
            {
                if (cartViewModel is not null)
                {
                    Cart existincart = await context.Carts.Where(o => o.Ordername == cartViewModel.Ordername).FirstOrDefaultAsync();
                    if (existincart is not null)
                    {
                        existincart.Quantity = cartViewModel.Quantity + existincart.Quantity;
                        context.Update(existincart);
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        string userid = await context.Users.Where(u => u.UserName == Name).Select(u => u.Id).FirstOrDefaultAsync();
                        Cart cart = new Cart()
                        {
                            Ordername = cartViewModel.Ordername,
                            Price = cartViewModel.Price,
                            imgpath = cartViewModel.imgpath,
                            Quantity = cartViewModel.Quantity,
                            UserId = userid
                        };
                        var result = await context.AddAsync(cart);
                        await context.SaveChangesAsync();
                    }
                    return "added";
                }
                return string.Empty;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteAllCartItems(string name)
        {
            try
            {
                if (name is not null)
                {
                    var id = context.Users.Where(o => o.UserName == name).Select(o => o.Id).FirstOrDefault();
                    if (id is not null)
                    {
                        var listcart = context.Carts.Where(o => o.UserId == id).ToList();
                        if (listcart is not null)
                        {
                            context.RemoveRange(listcart);
                            await context.SaveChangesAsync();
                            return "deletedAll";
                        }
                    }
                }
                return string.Empty;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteCartItem(int id, string name)
        {
            try
            {
                if (id != 0)
                {
                    var result = context.Carts.Where(o => o.Id == id).FirstOrDefault();
                    if (result is not null)
                    {
                        context.Remove(result);
                        await context.SaveChangesAsync();
                        return "deleted";
                    }
                }
                return string.Empty;
            }
            catch
            {
                throw;
            }
        }

    
        public async Task<List<CartUpdateViewModel>> GetAllUserCart(string Name)
        {
            try
            {
                if (Name is not null)
                {
                    string userid = await context.Users.Where(u => u.UserName == Name).Select(u => u.Id).FirstOrDefaultAsync();
                    if (userid is not null)
                    {
                        List<Cart> carts = await context.Carts.Where(o => o.UserId == userid).ToListAsync();
                        if (carts is not null)
                        {
                            List<CartUpdateViewModel> cart = new List<CartUpdateViewModel>();

                            foreach (var item in carts)
                            {
                                if (await context.Menus.Where(o => o.IsDeleted == false).Where(o => o.Name == item.Ordername).FirstOrDefaultAsync() == null)
                                {
                                    await DeleteCartItem(item.Id,Name);
                                    continue;
                                }
                                else
                                {
                                    cart.Add(new CartUpdateViewModel
                                    {
                                        Id = item.Id,
                                        UserId = userid,
                                        Quantity = item.Quantity,
                                        imgpath = item.imgpath,
                                        Price = item.Price,
                                        Ordername = item.Ordername,
                                        SubTotalPrice = item.TotalPrice
                                    });
                                }

                            }
                            return cart;
                        }
                    }
                }
                return new List<CartUpdateViewModel>();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> GetCartCount(string name)
        {
            if(!string.IsNullOrEmpty(name))
            {
                var id = await context.Users.Where(o => o.UserName == name).Select(o => o.Id).FirstOrDefaultAsync();
                int numberCart = await context.Carts.Where(o => o.UserId == id).CountAsync();
                return numberCart;
            }
            return 0;
        }

        public async Task<double> totalprice(string name)
        {
            try
            {
                if (name is not null)
                {
                    string userid = await context.Users.Where(u => u.UserName == name).Select(u => u.Id).FirstOrDefaultAsync();
                    if (userid is not null)
                    {
                        List<Cart> carts = await context.Carts.Where(o => o.UserId == userid).ToListAsync();
                        if (carts is not null)
                        {
                            double totalprice = 0;
                            foreach (var c in carts)
                            {
                                totalprice += c.TotalPrice;
                            }
                            return totalprice;
                        }
                    }
                }
                return double.NaN;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> UpdateCartItem(CartUpdateViewModel cartUpdateViewModel)
        {
            try
            {
                if (cartUpdateViewModel is not null)
                {
                    Cart cart = new Cart()
                    {
                        Id = cartUpdateViewModel.Id,
                        Ordername = cartUpdateViewModel.Ordername,
                        imgpath = cartUpdateViewModel.imgpath,
                        Price = cartUpdateViewModel.Price,
                        Quantity = cartUpdateViewModel.Quantity,
                        UserId = cartUpdateViewModel.UserId
                    };
                    var result = context.Update(cart);
                    await context.SaveChangesAsync();
                    if (result is not null)
                    {
                        return "updated";
                    }
                }
                return string.Empty;
            }
            catch
            {
                throw;
            }
        }
    }
}
