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
            OrderDetails dto = new OrderDetails();
            dto.productList = new List<ProductDTO>();
            ProductDTO prod;
            dto.OrderNum = "ORD#";
            dto.IsPaid = true;
            dto.TotalAmount = 250;
            dto.IsActive = true;
            dto.OrderStatus = "Pending";
            dto.CreatedOn = DateTime.Now;
            for (int i = 0; i < 3; i++)
            {
                prod = new ProductDTO();
                prod.Name = "Prod"+ i;
                prod.Price = i * 10 + 150;
                prod.IsActive = true;
                prod.PictureName = "abc";
                prod. CreatedBy = 1;
                prod.CreatedOn = DateTime.Now;
                
                prod.ModifiedBy = 1;
                prod.ModifiedOn = DateTime.Now;
                dto.productList.Insert(i,prod);
            }
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

        public static ResponseResult DeleteOrder(int oId)
        {
            return OrderDAO.DeleteOrder(oId);
        }

        public static ResponseResult UpdateOrderStatus(String status, int id)
        {
            return OrderDAO.UpdateOrderStatus(status,id);
        }
    }
}