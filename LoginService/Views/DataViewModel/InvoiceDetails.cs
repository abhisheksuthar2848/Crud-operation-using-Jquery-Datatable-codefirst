using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginService.Views.DataViewModel
{
    public class InvoiceDetails
    {
       
        

        public int InvoiceId { get; set; }
        public string InvoiceGuid { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string CustomerName { get; set; }


        public decimal Tax { get; set; }

        public decimal Discount { get; set; }
    }
}