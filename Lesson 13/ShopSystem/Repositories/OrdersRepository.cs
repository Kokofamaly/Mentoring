using ShopSystem.Infrastructure;
using ShopSystem.Infrastructure.Interfaces;
using ShopSystem.Models;
using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;
using ShopSystem.Enum;

namespace ShopSystem.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly string _connectionString;
        public OrdersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Order order)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("INSERT INTO [dbo].[Orders] (Status, CreatedDate, UpdatedDate, ProductId) VALUES (@Status, @CreatedDate, @UpdatedDate, @ProductId)", conn);

            cmd.Parameters.AddWithValue("@Status", order.Status.ToString());
            cmd.Parameters.AddWithValue("@CreatedDate", order.CreatedDate);
            cmd.Parameters.AddWithValue("@UpdatedDate", order.UpdatedDate);
            cmd.Parameters.AddWithValue("@ProductId", order.ProductId);

            cmd.ExecuteNonQuery();
        }

        public void Update(Order order)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("UPDATE [dbo].[Orders] SET Status = @Status, CreatedDate = @CreatedDate, UpdatedDate = @UpdatedDate, ProductId = @ProductId WHERE Id = @Id", conn);

            cmd.Parameters.AddWithValue("@Id", order.Id);
            cmd.Parameters.AddWithValue("@Status", order.Status.ToString());
            cmd.Parameters.AddWithValue("@CreatedDate", order.CreatedDate);
            cmd.Parameters.AddWithValue("@UpdatedDate", order.UpdatedDate);
            cmd.Parameters.AddWithValue("@ProductId", order.ProductId);

            cmd.ExecuteNonQuery();
        }
        public void Delete(Order order)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("DELETE FROM [dbo].[Orders] WHERE Id = @Id", conn);

            cmd.Parameters.AddWithValue("@Id", order.Id);

            cmd.ExecuteNonQuery();
        }
        public void DeleteBulk(OrderFilter filter)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("sp_DeleteOrders", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Year", filter.Year ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Month", filter.Month ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", filter.Status.ToString() ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ProductId", filter.ProductId ?? (object)DBNull.Value);

            cmd.ExecuteNonQuery();
        }
        public Order? GetById(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("SELECT * FROM [dbo].[Orders] WHERE Id = @Id", conn);

            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            
            var order = new Order
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                CreatedDate = reader.GetDateTimeOffset(reader.GetOrdinal("CreatedDate")),
                Status = System.Enum.Parse<OrderStatus>(reader.GetString(reader.GetOrdinal("Status"))),
                UpdatedDate = reader.GetDateTimeOffset(reader.GetOrdinal("UpdatedDate"))
            };

            return order;

        }
        public IEnumerable<Order> GetAll(OrderFilter? filter = null)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("sp_GetOrders", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Year", filter.Year ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Month", filter.Month ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", filter.Status.ToString() ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ProductId", filter.ProductId ?? (object)DBNull.Value);

            using var reader = cmd.ExecuteReader();

            var list = new List<Order>();

            while (reader.Read()) {

                var order = new Order
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                    CreatedDate = reader.GetDateTimeOffset(reader.GetOrdinal("CreatedDate")),
                    Status = System.Enum.Parse<OrderStatus>(reader.GetString(reader.GetOrdinal("Status"))),
                    UpdatedDate = reader.GetDateTimeOffset(reader.GetOrdinal("UpdatedDate"))
                };

                list.Add(order);
            
            }

            return list;
        }
    }
}
