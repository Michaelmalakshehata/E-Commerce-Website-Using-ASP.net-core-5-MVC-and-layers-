﻿using E_Commerce_Website.BL.IRepositories;
using E_Commerce_Website.DAL;
using E_Commerce_Website.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.Repositories
{

    public class GenericService<T> : IGenericRepository<T> where T : BaseModel
    {
        protected EntityContext _Context;
        protected DbSet<T> _Entities;
        public GenericService(EntityContext context)
        {
            _Context = context;
            _Entities = _Context.Set<T>();
        }
        async Task<List<T>> IGenericRepository<T>.Getall()
        {

            try
            {
                return await _Entities.Where(w => w.IsDeleted == false).ToListAsync();

            }
            catch
            {
                return new List<T>();
            }

        }


        async Task<T> IGenericRepository<T>.GetById(int id)
        {
            if (id == 0)
            {
                return default(T);
            }

            if (await _Entities.FindAsync(id) is null)
            {
                return default(T);

            }
            return await _Entities.FindAsync(id);
        }


        async Task<T> IGenericRepository<T> .add(T data)
        {
            if (data is null)
            {
                return default(T);
            }
            await _Context.AddAsync(data);
            await _Context.SaveChangesAsync();
            return data;

        }

        async Task<string> IGenericRepository<T>.update(T data,string name)
        {
            if (data is null &&name is null)
            {
                return string.Empty;

            }
            data.UpdateDate = DateTime.Now;
            data.UserName=name;
            var result=_Context.Update(data);
           await _Context.SaveChangesAsync();
            if(result is null)
            {
                return string.Empty;
            }
            return "Updated";
        }


        async Task<int> IGenericRepository<T>.delete(int id,string name)
        {
            if (id == 0)
            {
                return 0;
            }
            if (await _Entities.FindAsync(id) is null)
            {
                return 0;
            }
            T obj = await _Entities.FindAsync(id);
            obj.IsDeleted = true;
            obj.DeleteDate = DateTime.Now;
            obj.UserName = name;
            _Context.Update(obj);
            await _Context.SaveChangesAsync();
            return id;
        }

        public async Task<List<T>> Find(Expression<Func<T, bool>> match)
        {
            if (match is null)
            {
                return new List<T>();
            }
            return await _Entities.Where(match).Where(b => b.IsDeleted == false).ToListAsync();
        }

        public async Task<List<T>> GetallSoftDeleted()
        {
            try
            {
                return await _Entities.Where(w => w.IsDeleted == true).ToListAsync();

            }
            catch
            {
               return new List<T>();
            }
        }

        public async Task<int> Restordeleted(int id,string name)
        {
            if (id == 0 &&name is null)
            {
                return 0;
            }
            if (await _Entities.FindAsync(id) is null)
            {
                return 0;
            }
            T obj = await _Entities.FindAsync(id);
            obj.IsDeleted = false;
            obj.RestoreDate = DateTime.Now;
            obj.UserName = name;
            _Context.Update(obj);
            await _Context.SaveChangesAsync();
            return id;
        }
    }
}
