using PMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class ProductBO
    {
        public static int Save()
        {
            ProductDTO dto = new ProductDTO();
            dto.Name = "Burger";
            dto.PictureName = "HHH";
            dto.Price = 250;
            dto.IsActive = true;
            dto.CreatedBy = 1;
            dto.CreatedOn = DateTime.Now;

            return PMS.DAL.ProductDAO.Save(dto);
        }
        public static ProductDTO GetProductById(int pid)
        {
            return PMS.DAL.ProductDAO.GetProductById(pid);
        }
        public static List<ProductDTO> GetAllProducts()
        {
            return PMS.DAL.ProductDAO.GetAllProducts();
        }

        public static int DeleteProduct(int pid)
        {
            return PMS.DAL.ProductDAO.DeleteProduct(pid);
        }
    }
}