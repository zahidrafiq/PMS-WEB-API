using PMS.BAL;
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
        
        // GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}
        
        [HttpGet]
        public void Get()
        {
            var a = ProductBO.GetAllProducts();
        }

        [HttpGet]
        public void Save()
        {
            var a = ProductBO.Save();
        }

        [HttpGet]
        public void Del(int id)
        {
            var a = ProductBO.DeleteProduct(id);
        }

        [HttpGet]
        public void GetById(int id)
        {
            var a = ProductBO.GetProductById(id);
        }
    }
}
