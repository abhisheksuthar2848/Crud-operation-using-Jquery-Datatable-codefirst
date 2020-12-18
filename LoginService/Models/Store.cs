using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoginService.Models
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }
        public string StoreName { get; set; }
       
        public string StoreCreatedBy { get; set; }

      
        public DateTime? StoreCreatedDate { get; set; }
      
        public DateTime? StoreEditTime { get; set; }

      
        public string StoreEditedBy { get; set; }



    }
}