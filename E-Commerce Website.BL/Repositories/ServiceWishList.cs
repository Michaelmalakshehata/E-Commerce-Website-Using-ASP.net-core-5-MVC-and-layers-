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
    public class ServiceWishList : IServiceWishList
    {
        private readonly EntityContext context;
        public ServiceWishList(EntityContext context)
        {
            this.context = context;
        }
        public async Task<string> AddToWishList(int Id, string name)
        {
            try
            {
                if (Id != 0 && name is not null)
                {
                    Menus product = await context.Menus.Where(o => o.IsDeleted == false).Where(o => o.Id == Id).FirstOrDefaultAsync();
                    string existinwishlist = await context.WishLists.Where(o => o.Ordername == product.Name).Select(o => o.Ordername).FirstOrDefaultAsync();
                    if (existinwishlist is null)
                    {
                        string userid = await context.Users.Where(o => o.UserName == name).Select(o => o.Id).FirstOrDefaultAsync();
                        if (userid is not null && product is not null)
                        {
                            WishList wishList = new WishList()
                            {
                                Ordername = product.Name,
                                imgpath = product.imgpath,
                                Price = product.Price,
                                UserId = userid
                            };
                            await context.AddAsync(wishList);
                            await context.SaveChangesAsync();
                        }
                        await GetWishListCount(name);
                        return "added";

                    }
                    else
                    {
                        return "added";
                    }
                }
                return string.Empty;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteAllWishListItems(string name)
        {
            try
            {
                if (name is not null)
                {
                    var id = context.Users.Where(o => o.UserName == name).Select(o => o.Id).FirstOrDefault();
                    if (id is not null)
                    {
                        var listwishlist = context.WishLists.Where(o => o.UserId == id).ToList();
                        if (listwishlist is not null)
                        {
                            context.RemoveRange(listwishlist);
                            await context.SaveChangesAsync();
                            await GetWishListCount(name);
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

        public async Task<string> DeleteWishListItem(int id, string name)
        {
            try
            {
                if (id != 0)
                {
                    var result = context.WishLists.Where(o => o.Id == id).FirstOrDefault();
                    if (result is not null)
                    {
                        context.Remove(result);
                        await context.SaveChangesAsync();
                        await GetWishListCount(name);
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

        public async Task<List<WishList>> GetAllUserWishList(string Name)
        {
            try
            {
                if (Name is not null)
                {
                    string userid = await context.Users.Where(u => u.UserName == Name).Select(u => u.Id).FirstOrDefaultAsync();
                    if (userid is not null)
                    {
                        List<WishList> wishLists = await context.WishLists.Where(o => o.UserId == userid).ToListAsync();

                        if (wishLists is not null)
                        {
                            List<WishList> listwishList = new List<WishList>();
                            foreach (var itm in wishLists)
                            {
                                if (await context.Menus.Where(o => o.IsDeleted == false).Where(o => o.Name == itm.Ordername).FirstOrDefaultAsync() == null)
                                {
                                    await DeleteWishListItem(itm.Id, Name);
                                    continue;
                                }
                                else
                                {
                                    listwishList.Add(itm);
                                }

                            }
                            return listwishList;

                        }
                    }
                }
                return new List<WishList>();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> GetWishListCount(string name)
        {
            if (name is not null)
            {
                var id = await context.Users.Where(o => o.UserName.Equals(name)).Select(o => o.Id).FirstOrDefaultAsync();
                int number = await context.WishLists.Where(o => o.UserId == id).CountAsync();
                return number;
            }
            return 0;
        }
    }
}
