using System.Data.SqlClient;
using System.Data.Common;
using ShopSystem.Models;
using ShopSystem.Infrastructure.Interfaces;

namespace ShopSystem.Services
{
    public class OrdersManager
    {
        private readonly IOrdersRepository _ordersRepository;
        public OrdersManager(IOrdersRepository repo) { 
        
            _ordersRepository = repo;
        
        }
        public void CreateOrder() { }
        public IEnumerable<Order> GetOrders() { }
        public Order GetOrder() { }
        public void DeleteOrder() { }
        public void UpdateOrder() { }
    }
}
