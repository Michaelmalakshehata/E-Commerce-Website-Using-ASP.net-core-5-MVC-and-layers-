using E_Commerce_Website.BL.IRepositories;
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
    public class ServiceFinishedOrders:IServiceFinishOrder
    {
        private readonly IServiceProduct serviceMenu;
        private readonly EntityContext context;
        public ServiceFinishedOrders(IServiceProduct serviceMenu, EntityContext context)
        {
            this.context = context;
            this.serviceMenu = serviceMenu;
        }
        public async Task AddFinishedOrder(int id)
        {

            if (id != 0)
            {

                Orders orders = await context.Orders.FindAsync(id);
                if (orders != null)
                {
                    FinishedOrders finishedOrders = new FinishedOrders()
                    {
                        Username = orders.Username,
                        AddressDetailes = orders.AddressDetailes,
                        Email = orders.Email,
                        Phonenumber = orders.Phonenumber,
                        TotalPrice = orders.TotalPrice,
                        OrderDetails = orders.OrderDetails
                    };
                    context.FinishedOrders.Add(finishedOrders);
                    context.Orders.Remove(orders);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<FinishedOrders>> GetAllFinishedOrder()
        {
            List<FinishedOrders> orders = await context.FinishedOrders.ToListAsync();
            return orders;
        }

        public async Task<List<FinishedOrders>> GetAllFinishedOrderByName(string name)
        {
            if (name != null)
            {
                List<FinishedOrders> orders = await context.FinishedOrders.Where(o => o.Username == name).ToListAsync();
                return orders;
            }
            return null;
        }
    }
}
