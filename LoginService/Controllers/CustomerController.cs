using LoginService.Views.DataViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginService.Models;
using Rotativa;

namespace LoginService.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context = null;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }       
       
        //for testing perpuse
        public ActionResult Display()
        {
            return View();
        }
        //for testing perpuse
        public ActionResult TestTemplete()
        {
            return View();
        }

        // for testig
        public ActionResult testdemo()
        {
            return View();
        }

        public ActionResult temp()
        {
            return View();
        }


        public ActionResult ConvertToPDF()
        {
            var printpdf = new ActionAsPdf("Display");
            return printpdf;
        }





        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Insert(CustomerViewModel data)
        {
            Customer obj = new Customer();
            obj.CustomerName = data.CustomerName;
            obj.CustomerPhone = data.CustomerPhone;
            obj.CustomerAddress = data.CustomerAddress;
            obj.CustomerEmail = data.CustomerEmail;

            _context.Customers.Add(obj);
            _context.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);


           
        }
        [HttpGet]
        public JsonResult GetById(int id)
        {
            var data = _context.Customers.Find(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(CustomerViewModel obj)
        {
            var data = _context.Customers.Find(obj.CustomerId);


            data.CustomerName = obj.CustomerName;
            data.CustomerAddress = obj.CustomerAddress;
            data.CustomerEmail = obj.CustomerEmail;
            data.CustomerPhone = obj.CustomerPhone;
            _context.SaveChanges();

            var massage = "Update Sucessfully";
            return Json(massage, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var data = _context.Customers.Find(id);
            _context.Customers.Remove(data);
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
                List<Customer> data = _context.Customers.ToList();
                int totalRecords = data.Count;


                if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                {
                    data = data.Where(p => p.CustomerId.ToString().ToLower().Contains(search.ToLower()) ||
                        p.CustomerName.ToString().Contains(search.ToLower()) ||
                        p.CustomerAddress.ToString().Contains(search.ToLower()) ||
                        p.CustomerPhone.ToString().Contains(search.ToLower()) ||
                        p.CustomerEmail.ToString().Contains(search.ToLower())
                     ).ToList();
                }


            


                data = SortTableData(order, orderDir, data);
                int recFilter = data.Count;
                data = data.Skip(startRec).Take(pageSize).ToList();
                var modifiedData = data.Select(d =>
                    new
                    {
                        d.CustomerId,
                        d.CustomerName,
                        d.CustomerAddress,
                        d.CustomerPhone,
                        d.CustomerEmail
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
        private List<Customer> SortTableData(string order, string orderDir, List<Customer> data)
        {
            List<Customer> lst = new List<Customer>();
            try
            {
                switch (order)
                {
                    case "0":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.CustomerId).ToList()
                                                                                                 : data.OrderBy(p => p.CustomerId).ToList();
                        break;
                    case "1":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.CustomerName).ToList()
                                                                                                 : data.OrderBy(p => p.CustomerName).ToList();
                        break;
                    case "2":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.CustomerAddress).ToList()
                                                                                                 : data.OrderBy(p => p.CustomerAddress).ToList();
                        break;
                    case "3":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.CustomerPhone).ToList()
                                                                                                 : data.OrderBy(p => p.CustomerPhone).ToList();
                        break;
                    case "4":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.CustomerEmail).ToList()
                                                                                                   : data.OrderBy(p => p.CustomerEmail).ToList();
                        break;
                    default:
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.CustomerId).ToList()
                                                                                                 : data.OrderBy(p => p.CustomerId).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return lst;
        }


    }
}