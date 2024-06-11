using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBizApplication.model;

namespace MyBizApplication.service
{
    public interface IOrderRespository
    {
        void AddOrder(Order order);
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
        void UpdateOrderStatus(int orderId, string status);
    }
}