using E_Commerce_Website.BL.ViewModels;
using E_Commerce_Website.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.IRepositories
{
    public interface IServiceOrder
    {
        Task<OrderViewModel> CheckoutOrder(string name);
        Task<decimal> totalprice(string name);
        Task AddOrder(OrderViewModel order);
        Task<Orders> Order(string name);
        Task<List<Orders>> AllOrders();
        Task<List<string>> OrderDetailes(string name);
        Task DeleteOrder(int id);
    }
}
