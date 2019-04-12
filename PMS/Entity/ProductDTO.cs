using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Entity
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public String Name { get; set; }
        public double Price { get; set; }
        public String PictureName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsActive { get; set; }
    }
}