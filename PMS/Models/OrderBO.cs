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
        public static ResponseResult SaveOrder()
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
        public static ResponseResult GetOrderById(int oId)
        {
            return OrderDAO.GetOrderById(oId);
        }
        public static ResponseResult GetAllOrders()
        {
            return OrderDAO.GetAllOrders();
        }

        public static ResponseResult DeleteOrder(int pid)
        {
            return OrderDAO.DeleteOrder(pid);
        }
    }
}