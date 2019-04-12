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
        public ResponseResult Get()
        {
            var a = OrderBO.GetAllOrders();
            return a;
        }

        [HttpGet]
        public ResponseResult Save()
        {
            var a = OrderBO.SaveOrder();
            return a;
        }

        [HttpGet]
        public ResponseResult Del(int id)
        {
            var a = OrderBO.DeleteOrder(id);
            return a;
        }

        [HttpGet]
        public ResponseResult GetById(int id)
        {
            var a = OrderBO.GetOrderById(id);
            return a;
        }
    }
}
