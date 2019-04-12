using PMS.Entity;
using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PMS.Controllers
{
    public class OrderController : ApiController
    {
        [HttpGet]
        public ResponseResult GetOrders()
        {
            var a = OrderBO.GetAllOrders();
            return a;
        }

        [HttpGet]
        public ResponseResult SaveOrder()
        {
            var a = OrderBO.SaveOrder();
            return a;
        }

        [HttpGet]
        public ResponseResult CancelOrder(int id)
        {
            var a = OrderBO.DeleteOrder(id);
            return a;
        }

        [HttpGet]
        public ResponseResult UpdateOrderStatus(String status, int id)
        {
            return OrderBO.UpdateOrderStatus(status, id);
        }

        [HttpGet]
        public ResponseResult GetOrderById(int id)
        {
            var a = OrderBO.GetOrderById(id);
            return a;
        }
    }
}
