using System.Data.SqlClient;
using System.Data.Common;
using ShopSystem.Models;
using ShopSystem.Infrastructure.Interfaces;
using ShopSystem.Infrastructure;

namespace ShopSystem.Services
{
    public class OrdersManager
    {
        private readonly IOrdersRepository _ordersRepository;
        public OrdersManager(IOrdersRepository repo) { 
        
            _ordersRepository = repo;
        
        }
        public void CreateOrder(Order? order) {

            ArgumentNullException.ThrowIfNull(order);
            _ordersRepository.Add(order);

        }
        public IEnumerable<Order> GetOrders(OrderFilter? filter) { 
            
           return _ordersRepository.GetAll(filter);

        }
        public Order GetOrder(int id) {

            var order = _ordersRepository.GetById(id);
            if(order == null) throw new KeyNotFoundException();

            return order;
        }
        public void DeleteOrder(Order? order) {

            ArgumentNullException.ThrowIfNull(order);
            _ordersRepository.Delete(order);

        }
        public void DeleteBulkOrders(OrderFilter filter)
        {
            _ordersRepository.DeleteBulk(filter);
        }
        public void UpdateOrder(Order? order) {

            ArgumentNullException.ThrowIfNull(order);
            _ordersRepository.Update(order);

        }
    }
}
