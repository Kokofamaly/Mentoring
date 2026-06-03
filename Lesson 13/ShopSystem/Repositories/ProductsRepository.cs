using ShopSystem.Infrastructure.Interfaces;
using ShopSystem.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;

namespace ShopSystem.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly string _connectionString;

        public ProductsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Product product)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("INSERT INTO [dbo].[Products] (Name, Description, Weight, Height, Width, Length) VALUES (@Name, @Description, @Weight, @Height, @Width, @Length)", conn);

            AddProductParams(cmd, product);

            cmd.ExecuteNonQuery();
        }

        public void Update(Product product)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("UPDATE [dbo].[Products] SET Name = @Name, Description = @Description, Weight = @Weight, Height = @Height, Width = @Width, Length = @Length WHERE Id = @Id", conn);

            AddProductParams(cmd, product);

            cmd.ExecuteNonQuery();
        }
        public void Delete(Product product)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("DELETE FROM [dbo].[Products] WHERE Id = @Id", conn);

            cmd.Parameters.AddWithValue("@Id", product.Id);

            cmd.ExecuteNonQuery();
        }
        public Product? GetById(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("SELECT * FROM [dbo].[Products] WHERE Id = @Id", conn);

            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();

            var product = MapProduct(reader);

            return product;
        }
        public IEnumerable<Product> GetAll()
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("SELECT * FROM [dbo].[Products]", conn);
            using var reader = cmd.ExecuteReader();

            var products = new List<Product>();
            while (reader.Read())
            {
                var product = MapProduct(reader);

                products.Add(product);
            }

            return products;
        }

        private static Product MapProduct(SqlDataReader reader)
        {
            return new Product
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Description = reader.GetString(reader.GetOrdinal("Description")),
                Weight = reader.GetDecimal(reader.GetOrdinal("Weight")),
                Height = reader.GetDecimal(reader.GetOrdinal("Height")),
                Width = reader.GetDecimal(reader.GetOrdinal("Width")),
                Length = reader.GetDecimal(reader.GetOrdinal("Length"))
            };
        }

        private static void AddProductParams(SqlCommand cmd, Product product)
        {
            cmd.Parameters.AddWithValue("@Id", product.Id);
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Description", product.Description);
            cmd.Parameters.AddWithValue("@Weight", product.Weight);
            cmd.Parameters.AddWithValue("@Height", product.Height);
            cmd.Parameters.AddWithValue("@Width", product.Width);
            cmd.Parameters.AddWithValue("@Length", product.Length);
        }
    }
}
