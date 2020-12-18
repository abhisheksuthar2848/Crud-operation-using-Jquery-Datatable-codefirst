using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginService.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        public string InvoiceGuid { get; set; }
        public DateTime? InvoiceDate { get; set; }
       

      
        public decimal Tax { get; set; }

        public decimal Discount { get; set; }




        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }


        [ForeignKey("Store")]
        public int StoreId { get; set; }
        public Store Store { get; set; }

    }
}