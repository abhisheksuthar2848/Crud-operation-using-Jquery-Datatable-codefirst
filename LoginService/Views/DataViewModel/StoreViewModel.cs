using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginService.Views.DataViewModel
{
    public class StoreViewModel
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }

        public int StoreCreatedBy { get; set; }


        public DateTime StoreCreatedDate { get; set; }

        public DateTime StoreEditTime { get; set; }


        public int StoreEditedBy { get; set; }


    }
}