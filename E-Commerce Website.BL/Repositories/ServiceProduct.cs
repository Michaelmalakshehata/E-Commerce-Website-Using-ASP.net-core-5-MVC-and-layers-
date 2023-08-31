using E_Commerce_Website.BL.IRepositories;
using E_Commerce_Website.BL.ViewModels;
using E_Commerce_Website.DAL;
using E_Commerce_Website.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.Repositories
{
    public class ServiceProduct : IServiceProduct
    {
        private readonly EntityContext context;
        private readonly IGenericRepository<Menus> genericRepository;
        private readonly IHostingEnvironment _hosting;
        public ServiceProduct(IGenericRepository<Menus> genericRepository, EntityContext context, IHostingEnvironment hosting)
        {
            this.genericRepository = genericRepository;
            this.context = context;
            this._hosting = hosting;
        }
        public async Task<Menus> add(ProductViewModel productViewModel,string name)
        {
            try
            {
                if (productViewModel is not null && name is not null)
                {
                    Random rnd=new Random();
                    string filename = string.Empty;
                    string filename2 = string.Empty;
                    string filename3 = string.Empty;
                    if (productViewModel.File is not null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, ("uploaded_img"));
                        filename = Guid.NewGuid().ToString() + "_" + productViewModel.File.FileName;
                        string fullpath =Path.Combine(uploads, filename);
                        productViewModel.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }
                    if (productViewModel.File2 is not null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, ("uploaded_img"));
                        filename2 = Guid.NewGuid().ToString() + "_" + productViewModel.File2.FileName;
                        string fullpath = Path.Combine(uploads, filename2);
                        productViewModel.File2.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }
                    if (productViewModel.File3 is not null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, ("uploaded_img"));
                        filename3 = Guid.NewGuid().ToString() + "_" + productViewModel.File3.FileName;
                        string fullpath = Path.Combine(uploads, filename3);
                        productViewModel.File3.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }
                    Menus menu = new Menus()
                    {
                        Name = productViewModel.Name,
                        Price = productViewModel.Price,
                        Detailes = productViewModel.Detailes,
                        imgpath = filename,
                        imgpath2 = filename2,
                        imgpath3 = filename3,
                        CategoryId = productViewModel.CategoryId,
                        CreateUserName = name
                    };
                    return await genericRepository.add(menu);
                }
                return new Menus();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> Delete(int id,string name)
        {
            try
            {
                if (id > 0&& name is not null)
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

        public async Task<List<Menus>> GetalldeletedProducts()
        {
            try
            {
                List<Menus> list = await genericRepository.GetallSoftDeleted();
                if (list is not null)
                {
                    return list;
                }
                else
                {
                    return new List<Menus>();
                }
            }
            catch
            {
                throw;
            }

        }

        public async Task<List<Menus>> GetallProducts()
        {
            try
            {
                List<Menus> list = await context.Menus.Where(e => e.IsDeleted == false).Include(e => e.Categories).ToListAsync();
                if (list is not null)
                {
                    return list;
                }
                else
                {
                    return new List<Menus>();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductUpdateViewModel> GetByid(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await genericRepository.GetById(id);
                    if (result is not null)
                    {
                        ProductUpdateViewModel productUpdateViewModel = new ProductUpdateViewModel()
                        {
                            Name = result.Name,
                            Id = result.Id,
                            Price = result.Price,
                            imgpath = result.imgpath,
                            imgpath2 = result.imgpath2,
                            imgpath3 = result.imgpath3,
                            Detailes = result.Detailes,
                            CategoryId = result.CategoryId
                        };
                        return productUpdateViewModel;
                    }

                }
                return new ProductUpdateViewModel();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Menus>> GetProductByCategory(int id)
        {
            try
            {
                if (id != 0)
                {
                    return await context.Menus.Where(s => s.IsDeleted == false).Where(s => s.CategoryId == id).ToListAsync();
                }
                return new List<Menus>();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> RestoreProduct(int id,string name)
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

        public void Update(ProductUpdateViewModel productUpdateViewModel,string name)
        {
            try
            {
                if (productUpdateViewModel is not null && name is not null)
                {
                    string filename = string.Empty;
                    string filename2 = string.Empty;
                    string filename3 = string.Empty;

                    Menus oldobj = context.Menus.Where(o => o.IsDeleted == false).Where(o => o.Id == productUpdateViewModel.Id).FirstOrDefault();
                    if (productUpdateViewModel.File is not null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, ("uploaded_img"));
                        filename = Guid.NewGuid().ToString() + "_" +productUpdateViewModel.File.FileName;
                        string fullpath = Path.Combine(uploads, filename);
                        if (oldobj.imgpath is not null)
                        {
                            string oldfilename = oldobj.imgpath;
                            string oldfullpath = Path.Combine(uploads, oldfilename);
                            System.IO.File.Delete(oldfullpath);
                        }
                        productUpdateViewModel.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }
                    else
                    {
                        filename = oldobj.imgpath;
                    }
                    if (productUpdateViewModel.File2 is not null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, ("uploaded_img"));
                        filename2 = Guid.NewGuid().ToString() + "_" +productUpdateViewModel.File2.FileName;
                        string fullpath = Path.Combine(uploads, filename2);
                        if (oldobj.imgpath2 != null)
                        {
                            string oldfilename = oldobj.imgpath2;
                            string oldfullpath = Path.Combine(uploads, oldfilename);
                            System.IO.File.Delete(oldfullpath);
                        }
                        productUpdateViewModel.File2.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }
                    else
                    {
                        filename2 = oldobj.imgpath2;
                    }
                    if (productUpdateViewModel.File3 is not null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, ("uploaded_img"));
                        filename3 = Guid.NewGuid().ToString() + "_" +productUpdateViewModel.File3.FileName;
                        string fullpath = Path.Combine(uploads, filename3);
                        if (oldobj.imgpath3 is not null)
                        {
                            string oldfilename = oldobj.imgpath3;
                            string oldfullpath = Path.Combine(uploads, oldfilename);
                            System.IO.File.Delete(oldfullpath);
                        }
                        productUpdateViewModel.File3.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }
                    else
                    {
                        filename3 = oldobj.imgpath3;
                    }
                    if (context.Menus.Where(s => s.Id != oldobj.Id).Where(s => s.Name == productUpdateViewModel.Name).Count() > 0)
                    {
                        throw new ArgumentNullException();
                    }
                    oldobj.Name = productUpdateViewModel.Name;
                    oldobj.Price = productUpdateViewModel.Price;
                    oldobj.Detailes = productUpdateViewModel.Detailes;
                    oldobj.imgpath = filename;
                    oldobj.imgpath2 = filename2;
                    oldobj.imgpath3 = filename3;
                    oldobj.CategoryId = productUpdateViewModel.CategoryId;
                    genericRepository.update(oldobj,name);
                }
            }
            catch
            {
                throw;
            }
        }
        
    }
}
