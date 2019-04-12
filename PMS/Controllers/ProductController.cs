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
    public class ProductController : ApiController
    {
        [HttpGet]
        public ResponseResult GetAllProducts()
        {
            var a = ProductBO.GetAllProducts();
            return a;
        }

        [HttpGet]
        public ResponseResult AddNewProduct()
        {
            var a = ProductBO.Save();
            return a;
        }

        [HttpGet]
        public ResponseResult DeleteProduct(int id)
        {
            var a = ProductBO.DeleteProduct(id);
            return a;
        }

        [HttpGet]
        public ResponseResult GetProductById(int id)
        {
            var a = ProductBO.GetProductById(id);
            return a;
        }

        [HttpGet]
        public ResponseResult GetProductsByOrderId(int id)
        {
            var a = ProductBO.GetProductsByOrderId(id);
            return a;
        }
    }
}
