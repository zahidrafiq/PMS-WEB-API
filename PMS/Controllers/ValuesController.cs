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
    public class ValuesController : ApiController
    {
        [HttpGet]
        public ResponseResult Get()
        {
            var a = ProductBO.GetAllProducts();
            return a;
        }

        [HttpGet]
        public ResponseResult Save()
        {
            var a = ProductBO.Save();
            return a;
        }

        [HttpGet]
        public ResponseResult Del(int id)
        {
            var a = ProductBO.DeleteProduct(id);
            return a;
        }

        [HttpGet]
        public ResponseResult GetById(int id)
        {
            var a = ProductBO.GetProductById(id);
            return a;
        }
    }
}
