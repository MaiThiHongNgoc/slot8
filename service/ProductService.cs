using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBizApplication.model;
using MySql.Data.MySqlClient;

namespace MyBizApplication.service
{
    public class ProductService : IProductRespository
    {
        private MySqlConnection conn;
        public ProductService(string connectionString){
            conn=new MySqlConnection(connectionString);
        }
        public void AddProduct(Product product)
        {
            //throw new NotImplementedException();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText="insert into products(name,price,description) values(@name,@price,@description)";
            cmd.Parameters.AddWithValue("@name",product.Name);
            cmd.Parameters.AddWithValue("@price",product.Price);
            cmd.Parameters.AddWithValue("@description",product.Description);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllProducts()
        {
            //throw new NotImplementedException();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText="select * from products";
            MySqlDataReader reader=cmd.ExecuteReader();
            List<Product> products=new List<Product>();
            while(reader.Read()){
                Product product=new Product();
                product.Id=reader.GetInt32("id");
                product.Name=reader.GetString("name");
                product.Price=reader.GetDecimal("price");
                product.Description=reader.GetString("description");
                products.Add(product);
            }
            conn.Close();
            return products;
        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}