using E_Commerce_Website.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.IRepositories
{
    public interface IServiceFinishOrder
    {
        Task<List<FinishedOrders>> GetAllFinishedOrder();
        Task<List<FinishedOrders>> GetAllFinishedOrderByName(string name);

        Task AddFinishedOrder(int id);
    }
}
