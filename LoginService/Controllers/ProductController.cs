using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginService.Models;
using LoginService.Views.DataViewModel;

namespace LoginService.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context = null;

        public ProductController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Insert(ProductViewModel data)
        {
            Product obj = new Product();
            obj.ProductName = data.ProductName;
            obj.ProductPrice = data.ProductPrice;

            _context.Products.Add(obj);
            _context.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);



        }
        [HttpGet]
        public JsonResult GetById(int id)
        {
            var data = _context.Products.Find(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(ProductViewModel obj)
        {
            var data = _context.Products.Find(obj.ProductId);


            data.ProductName = obj.ProductName;
            data.ProductPrice = obj.ProductPrice;

            _context.SaveChanges();

            var massage = "Update Sucessfully";
            return Json(massage, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var data = _context.Products.Find(id);
            _context.Products.Remove(data);
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
                List<Product> data = _context.Products.ToList();
                int totalRecords = data.Count;


                if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                {
                    data = data.Where(p => p.ProductId.ToString().ToLower().Contains(search.ToLower()) ||
                        p.ProductName.ToString().Contains(search.ToLower()) ||
                        p.ProductPrice.ToString().Contains(search.ToLower())
                     ).ToList();
                }


             

                data = SortTableData(order, orderDir, data);
                int recFilter = data.Count;
                data = data.Skip(startRec).Take(pageSize).ToList();
                var modifiedData = data.Select(d =>
                    new
                    {
                        d.ProductId,
                        d.ProductName,
                        d.ProductPrice

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
        private List<Product> SortTableData(string order, string orderDir, List<Product> data)
        {
            List<Product> lst = new List<Product>();
            try
            {
                switch (order)
                {
                    case "0":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ProductId).ToList()
                                                                                                 : data.OrderBy(p => p.ProductId).ToList();
                        break;
                    case "1":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ProductName).ToList()
                                                                                                 : data.OrderBy(p => p.ProductName).ToList();
                        break;
                    case "2":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ProductPrice).ToList()
                                                                                                 : data.OrderBy(p => p.ProductPrice).ToList();
                        break;

                    default:
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ProductId).ToList()
                                                                                                 : data.OrderBy(p => p.ProductId).ToList();

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