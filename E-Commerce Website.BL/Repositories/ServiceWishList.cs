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
            if (Id !=0 && name != null)
            {
                Menus product = await context.Menus.Where(o => o.IsDeleted == false).Where(o => o.Id == Id).FirstOrDefaultAsync();
                string existinwishlist = await context.WishLists.Where(o=>o.Ordername==product.Name).Select(o=>o.Ordername).FirstOrDefaultAsync();
                if (existinwishlist == null)
                {
                    string userid = await context.Users.Where(o => o.UserName == name).Select(o => o.Id).FirstOrDefaultAsync();
                    if (userid != null && product != null)
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
                        return "added";
                    
                }
                else
                {
                    return "added";
                }
            }
            return null;
        }

        public async Task<string> DeleteAllWishListItems(string name)
        {
            if (name != null)
            {
                var id = context.Users.Where(o => o.UserName == name).Select(o => o.Id).FirstOrDefault();
                if (id != null)
                {
                    var listwishlist = context.WishLists.Where(o => o.UserId == id).ToList();
                    if (listwishlist != null)
                    {
                        context.RemoveRange(listwishlist);
                        await context.SaveChangesAsync();
                        return "deletedAll";
                    }
                }
            }
            return null;
        }

        public async Task<string> DeleteWishListItem(int id)
        {
            if (id != 0)
            {
                var result = context.WishLists.Where(o => o.Id == id).FirstOrDefault();
                if (result != null)
                {
                    context.Remove(result);
                    await context.SaveChangesAsync();
                    return "deleted";
                }
            }
            return null;
        }

        public async Task<List<WishList>> GetAllUserWishList(string Name)
        {
            if (Name != null)
            {
                string userid = await context.Users.Where(u => u.UserName == Name).Select(u => u.Id).FirstOrDefaultAsync();
                if (userid != null)
                {
                    List<WishList> wishLists = await context.WishLists.Where(o => o.UserId == userid).ToListAsync();

                    if (wishLists != null)
                    {
                        List<WishList> listwishList = new List<WishList>();     
                        foreach (var itm in wishLists)
                        { 
                            if(await context.Menus.Where(o=>o.IsDeleted==false).Where(o=>o.Name==itm.Ordername).FirstOrDefaultAsync()==null)
                            {
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
            return null;
        }
    }
}
