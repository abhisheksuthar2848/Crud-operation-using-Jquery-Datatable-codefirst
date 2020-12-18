using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginService.Models;
using LoginService.Views.DataViewModel;


namespace LoginService.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context = null;

        public InvoiceController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult invoiceAdd()
        {
            return View();
        }


        public JsonResult Insert(InvoiceViewModel data)
        {

            Guid obj = Guid.NewGuid();

            Invoice invoice = new Invoice();
            invoice.InvoiceDate = DateTime.Now;
            invoice.Tax = data.Tax;
            invoice.Discount = data.Discount;
            invoice.CustomerId = data.CustomerId;
            invoice.StoreId = data.StoreId;
            invoice.InvoiceGuid = obj.ToString();

            _context.Invoices.Add(invoice);
            _context.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);



        }
        [HttpGet]
        public JsonResult GetById(int id)
        {
            var data = _context.Invoices.Find(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(InvoiceViewModel obj)
        {
            var data = _context.Invoices.Find(obj.InvoiceId);


            data.InvoiceDate = DateTime.Now;
            //data.InvoiceGuid = obj.InvoiceGuid;  no update guid
            data.Tax = obj.Tax;
            data.Discount = obj.Discount;
            data.CustomerId = obj.CustomerId;
            data.StoreId = obj.StoreId;


            _context.SaveChanges();

            var massage = "Update Sucessfully";
            return Json(massage, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var data = _context.Invoices.Find(id);
            _context.Invoices.Remove(data);
            _context.SaveChanges();

            var massage = "Delete Sucessfully";
            return Json(massage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetTableData()
        {
            JsonResult result = new JsonResult();
            try
            {
                string search = Request.Form.GetValues("search[value]")[0];
                string draw = Request.Form.GetValues("draw")[0];
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];

                int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
                int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);


                List<InvoiceViewModel> data = new List<InvoiceViewModel>();
                data = (from in1 in _context.Invoices
                            join c1 in _context.Customers on in1.CustomerId equals c1.CustomerId
                            join s1 in _context.Stores on in1.StoreId equals s1.StoreId
                            select new InvoiceViewModel
                            {
                                InvoiceId = in1.InvoiceId,
                                InvoiceGuid = in1.InvoiceGuid,
                                InvoiceDate = in1.InvoiceDate,
                                StoreId = in1.StoreId,
                                StoreName = s1.StoreName,
                                CustomerName = c1.CustomerName,
                                Tax = in1.Tax,
                                CustomerId=c1.CustomerId,
                                Discount = in1.Discount

                            }).ToList();






                //List<Invoice> data = _context.Invoices.ToList();
                int totalRecords = data.Count;


                if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                {
                    data = data.Where(p => p.InvoiceId.ToString().ToLower().Contains(search.ToLower()) ||
                        p.InvoiceGuid.ToString().Contains(search.ToLower()) ||
                        p.InvoiceDate.ToString().Contains(search.ToLower()) ||
                        p.Tax.ToString().Contains(search.ToLower()) ||
                        p.Discount.ToString().Contains(search.ToLower()) ||
                        p.CustomerName.ToString().Contains(search.ToLower()) ||
                        p.StoreName.ToString().Contains(search.ToLower())
                     ).ToList();
                }




                data = SortTableData(order, orderDir, data);
                int recFilter = data.Count;
                data = data.Skip(startRec).Take(pageSize).ToList();
                var modifiedData = data.Select(d =>
                    new
                    {
                        d.InvoiceId,
                        d.InvoiceGuid,
                        d.InvoiceDate,
                        d.Tax,
                        d.Discount,
                        d.StoreId,
                        d.StoreName,
                        d.CustomerId,
                        d.CustomerName

                    }
                    );
                result = this.Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = totalRecords,
                    recordsFiltered = recFilter,
                    data = modifiedData
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return result;
        }
        private List<InvoiceViewModel> SortTableData(string order, string orderDir, List<InvoiceViewModel> data)
        {
            List<InvoiceViewModel> lst = new List<InvoiceViewModel>();
            try
            {
                switch (order)
                {
                    case "0":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.InvoiceId).ToList()
                                                                                                 : data.OrderBy(p => p.InvoiceId).ToList();
                        break;
                    case "1":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.InvoiceGuid).ToList()
                                                                                                 : data.OrderBy(p => p.InvoiceGuid).ToList();
                        break;
                    case "2":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.InvoiceDate).ToList()
                                                                                                 : data.OrderBy(p => p.InvoiceDate).ToList();
                        break;
                    case "3":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Tax).ToList()
                                                                                                 : data.OrderBy(p => p.Tax).ToList();
                        break;
                    case "4":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Discount).ToList()
                                                                                                 : data.OrderBy(p => p.Discount).ToList();
                        break;
                    case "5":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.CustomerName).ToList()
                                                                                                 : data.OrderBy(p => p.CustomerName).ToList();
                        break;

                    case "6":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.StoreName).ToList()
                                                                                                 : data.OrderBy(p => p.StoreName).ToList();

                        break;
                    default:
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.InvoiceId).ToList()
                                                                                                 : data.OrderBy(p => p.InvoiceId).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return lst;
        }


        public JsonResult GetCustomerList()
        {
            var data = _context.Customers.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStoreList()
        {
            var data = _context.Stores.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductList()
        {
            var data = _context.Products.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}