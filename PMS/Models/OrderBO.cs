using PMS.DAL;
using PMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class OrderBO
    {
        public static int SaveOrder()
        {
            OrderDTO dto = new OrderDTO();
            dto.OrderNum = "ORD#1";
            dto.IsPaid = true;
            dto.TotalAmount = 250;
            dto.IsActive = true;
            dto.OrderStatus = 0;
            dto.CreatedOn = DateTime.Now;

            return PMS.DAL.OrderDAO.SaveOrder(dto);
        }
        public static OrderDTO GetOrderById(int oId)
        {
            return OrderDAO.GetOrderById(oId);
        }
        public static List<OrderDTO> GetAllOrders()
        {
            return OrderDAO.GetAllOrders();
        }

        public static int DeleteOrder(int pid)
        {
            return OrderDAO.DeleteOrder(pid);
        }
    }
}