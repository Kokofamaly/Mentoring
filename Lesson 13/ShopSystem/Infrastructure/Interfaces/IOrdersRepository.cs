using ShopSystem.Infrastructure;
using ShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Infrastructure.Interfaces
{
    public interface IOrdersRepository
    {
        public void Add(Order order);
        public void Update(Order order);
        public void Delete(Order order);
        public void DeleteBulk(OrderFilter filter);
        public Order? GetById(int id);
        public IEnumerable<Order> GetAll(OrderFilter? filter);
    }
}
