using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginService.Views.DataViewModel
{
    public class InvoiceViewModel
    {
        public int InvoiceId { get; set; }
        public string InvoiceGuid { get; set; }
        public DateTime? InvoiceDate { get; set; }



        public decimal Tax { get; set; }

        public decimal Discount { get; set; }
        public int CustomerId { get; set; }
        public string  CustomerName { get; set; }

        public int StoreId { get; set; }
        public string StoreName { get; set; }
    }
}