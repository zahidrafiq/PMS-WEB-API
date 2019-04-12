using PMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class ProductBO
    {
        public static ResponseResult Save()
        {
            ProductDTO dto = new ProductDTO();
            dto.Name = "Burger";
            dto.PictureName = "HHH";
            dto.Price = 250;
            dto.IsActive = true;
            dto.CreatedBy = 1;
            dto.CreatedOn = DateTime.Now;
            try
            {
                return ResponseResult.GetSuccessObject( PMS.DAL.ProductDAO.Save(dto));
            }
            catch(Exception exp)
            {
                return ResponseResult.GetErrorObject("Some Error has been Occured! " + exp);
            }
        }

        public static ResponseResult GetProductById(int pid)
        {
            return PMS.DAL.ProductDAO.GetProductById(pid);
        }
        public static ResponseResult GetAllProducts()
        {
            return PMS.DAL.ProductDAO.GetAllProducts();
        }

        public static ResponseResult DeleteProduct(int pid)
        {
            return PMS.DAL.ProductDAO.DeleteProduct(pid);
        }
    }
}