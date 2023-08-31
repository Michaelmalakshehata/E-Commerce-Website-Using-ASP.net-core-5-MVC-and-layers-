using E_Commerce_Website.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.IRepositories
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        Task<List<T>> Getall();
        Task<List<T>> GetallSoftDeleted();
        Task<T> GetById(int id);
        Task<T> add(T data);
        Task update(T data,string name);
        Task<int> delete(int id,string name);
        Task<int> Restordeleted(int id,string name);
        Task<List<T>> Find(Expression<Func<T, bool>> match);

    }
}
