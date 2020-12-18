using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoginService.Models
{
    public class SpGetInvoiceList
    {
        [Key]
        public int invoicelist { get; set; }
        public int InvoiceId { get; set; }
        public string InvoiceGuid { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal Tax { get; set; }

        public decimal Discount { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }

        public string StoreCreatedBy { get; set; }


        public DateTime? StoreCreatedDate { get; set; }

        public DateTime? StoreEditTime { get; set; }


        public string StoreEditedBy { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }



    }
}