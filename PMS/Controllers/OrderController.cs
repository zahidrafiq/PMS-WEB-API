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
        public void Get()
        {
            var a = OrderBO.GetAllOrders();
        }

        [HttpGet]
        public void Save()
        {
            var a = OrderBO.SaveOrder();
        }

        [HttpGet]
        public void Del(int id)
        {
            var a = OrderBO.DeleteOrder(id);
        }

        [HttpGet]
        public void GetById(int id)
        {
            var a = OrderBO.GetOrderById(id);
        }
    }
}
