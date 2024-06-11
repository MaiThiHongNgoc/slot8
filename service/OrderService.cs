using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBizApplication.model;
using MySql.Data.MySqlClient;

namespace MyBizApplication.service
{
    public class OrderService : IOrderRespository
    {
        private readonly string connectionString;

        public OrderService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddOrder(Order order)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.Transaction = transaction;
                    cmd.CommandText = "insert into orders(customer_id, order_date, status) values(@customer_id, @order_date, @status)";
                    cmd.Parameters.AddWithValue("@customer_id", order.CustomerId);
                    cmd.Parameters.AddWithValue("@order_date", order.OrderDate);
                    cmd.Parameters.AddWithValue("@status", order.Status ?? "Pending");
                    cmd.ExecuteNonQuery();
                    int orderId = (int)cmd.LastInsertedId;

                    foreach (var detail in order.OrderDetails)
                    {
                        MySqlCommand detailCmd = conn.CreateCommand();
                        detailCmd.Transaction = transaction;
                        detailCmd.CommandText = "insert into order_details(order_id, product_id, quantity) values(@order_id, @product_id, @quantity)";
                        detailCmd.Parameters.AddWithValue("@order_id", orderId);
                        detailCmd.Parameters.AddWithValue("@product_id", detail.ProductId);
                        detailCmd.Parameters.AddWithValue("@quantity", detail.Quantity);
                        detailCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from orders";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order();
                        order.Id = reader.GetInt32("id");
                        order.CustomerId = reader.GetInt32("customer_id");
                        order.OrderDate = reader.GetDateTime("order_date");
                        order.Status = reader.GetString("status");
                        order.OrderDetails = new List<OrderDetail>();
                        orders.Add(order);
                    }
                }

                foreach (var order in orders)
                {
                    MySqlCommand detailCmd = conn.CreateCommand();
                    detailCmd.CommandText = "select * from order_details where order_id=@order_id";
                    detailCmd.Parameters.AddWithValue("@order_id", order.Id);
                    using (MySqlDataReader detailReader = detailCmd.ExecuteReader())
                    {
                        while (detailReader.Read())
                        {
                            OrderDetail detail = new OrderDetail();
                            detail.ProductId = detailReader.GetInt32("product_id");
                            detail.Quantity = detailReader.GetInt32("quantity");
                            order.OrderDetails.Add(detail);
                        }
                    }
                }

                conn.Close();
            }
            return orders;
        }

        public void UpdateOrderStatus(int orderId, string status)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE orders SET status = @status WHERE id = @orderId";
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@orderId", orderId);
                cmd.ExecuteNonQuery();
            }
        }

        public Order GetOrderById(int id)
        {
            throw new NotImplementedException();
        }
    }
}