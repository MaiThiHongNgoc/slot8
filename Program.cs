using System.Data;
using MyBizApplication.Controller;
using MyBizApplication.model;
using MyBizApplication.service;

namespace MyBizApplication
{
    class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = "server=127.0.0.1; database=prodb; user=root; password=";
            IProductRespository productService = new ProductService(connectionString);
            ProductController productController = new ProductController(productService);

            IOrderRespository orderService = new OrderService(connectionString);
            OrderController orderController = new OrderController(orderService);

            while (true)
            {
                Console.WriteLine("My Biz Application");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Display all products");
                Console.WriteLine("3. Add Order");
                Console.WriteLine("4. Display all orders");
                Console.WriteLine("5. Update order status");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter product name: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter product price: ");
                        decimal price = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Enter product description: ");
                        string description = Console.ReadLine();
                        Product newProduct = new Product { Name = name, Price = price, Description = description };
                        productController.AddProduct(newProduct);
                        Console.WriteLine("Product added successfully!!!");
                        break;

                    case 2:
                        List<Product> products = productController.GetAllProducts();
                        foreach (var product in products)
                        {
                            Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price}, Description: {product.Description}");
                        }
                        break;

                    case 3:
                        Console.WriteLine("Enter customer Id: ");
                        int orderCustomerId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter number of products: ");
                        int numberOfProducts = Convert.ToInt32(Console.ReadLine());
                        List<OrderDetail> orderDetails = new List<OrderDetail>();

                        for (int i = 0; i < numberOfProducts; i++)
                        {
                            Console.WriteLine($"Enter product Id for product {i + 1}: ");
                            int productId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter quantity: ");
                            int quantity = Convert.ToInt32(Console.ReadLine());
                            OrderDetail orderDetail = new OrderDetail { ProductId = productId, Quantity = quantity };
                            orderDetails.Add(orderDetail);
                        }

                        Order newOrder = new Order { CustomerId = orderCustomerId, OrderDate = DateTime.Now, OrderDetails = orderDetails };
                        orderController.AddOrder(newOrder);
                        Console.WriteLine("Order added successfully!!!");
                        break;

                    case 4:
                        List<Order> orders = orderController.GetAllOrders();
                        foreach (var order in orders)
                        {
                            Console.WriteLine($"Order Id: {order.Id}, CustomerId: {order.CustomerId}, OrderDate: {order.OrderDate}, Status: {order.Status}");
                            foreach (var detail in order.OrderDetails)
                            {
                                Console.WriteLine($"Product Id: {detail.ProductId}, Quantity: {detail.Quantity}");
                            }
                        }
                        break;

                    case 5:
                        Console.WriteLine("Enter order Id: ");
                        int orderId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter new status (Pending, Processing, Completed, Cancelled): ");
                        string status = Console.ReadLine();
                        orderController.UpdateOrderStatus(orderId, status);
                        Console.WriteLine("Order status updated successfully!!!");
                        break;

                    case 6:
                        return;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
    }
}