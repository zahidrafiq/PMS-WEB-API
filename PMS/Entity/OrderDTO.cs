using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Entity
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public String OrderNum { get; set; }
        public int OrderBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Boolean IsPaid { get; set; }
        public double TotalAmount { get; set; }
        public Boolean IsActive { get; set; }
        public int OrderStatus { get; set; }

    }
}