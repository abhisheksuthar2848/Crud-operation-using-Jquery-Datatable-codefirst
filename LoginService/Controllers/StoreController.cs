using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginService.Models;
using LoginService.Views.DataViewModel;
using Microsoft.AspNet.Identity;

namespace LoginService.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        private readonly ApplicationDbContext _context = null;

        public StoreController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Insert(StoreViewModel data)
        {
            Store obj = new Store();
            obj.StoreName = data.StoreName;
          
            obj.StoreCreatedBy =User.Identity.GetUserId();
            obj.StoreCreatedDate = DateTime.Now;
           

            _context.Stores.Add(obj);
            _context.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);



        }
        [HttpGet]
        public JsonResult GetById(int id)
        {
            var data = _context.Stores.Find(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(StoreViewModel obj)
        {
            var data = _context.Stores.Find(obj.StoreId);



            data.StoreName = obj.StoreName;

            data.StoreEditedBy = User.Identity.GetUserId();
            data.StoreEditTime = DateTime.Now;

            _context.SaveChanges();

            var massage = "Update Sucessfully";
            return Json(massage, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var data = _context.Stores.Find(id);
            _context.Stores.Remove(data);
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
                List<Store> data = _context.Stores.ToList();
                int totalRecords = data.Count;


                if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                {
                    data = data.Where(p => p.StoreId.ToString().ToLower().Contains(search.ToLower()) ||
                        p.StoreName.ToString().Contains(search.ToLower()) ||
                        p.StoreCreatedBy.ToString().Contains(search.ToLower()) ||
                        p.StoreCreatedDate.ToString().Contains(search.ToLower()) ||
                        p.StoreEditedBy.ToString().Contains(search.ToLower()) ||
                        p.StoreEditTime.ToString().Contains(search.ToLower())

                     ).ToList();
                }





                data = SortTableData(order, orderDir, data);
                int recFilter = data.Count;
                data = data.Skip(startRec).Take(pageSize).ToList();
                var modifiedData = data.Select(d =>
                    new
                    {
                        d.StoreId,
                        d.StoreName,
                        d.StoreCreatedBy,
                        d.StoreCreatedDate,
                        d.StoreEditTime,
                        d.StoreEditedBy

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
        private List<Store> SortTableData(string order, string orderDir, List<Store> data)
        {
            List<Store> lst = new List<Store>();
            try
            {
                switch (order)
                {
                    case "0":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.StoreId).ToList()
                                                                                                 : data.OrderBy(p => p.StoreId).ToList();
                        break;
                    case "1":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.StoreName).ToList()
                                                                                                 : data.OrderBy(p => p.StoreName).ToList();
                        break;
                    case "2":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.StoreCreatedBy).ToList()
                                                                                                 : data.OrderBy(p => p.StoreCreatedBy).ToList();
                        break;
                    case "3":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.StoreCreatedDate).ToList()
                                                                                                 : data.OrderBy(p => p.StoreCreatedDate).ToList();
                        break;
                    case "4":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.StoreEditTime).ToList()
                                                                                                 : data.OrderBy(p => p.StoreEditTime).ToList();
                        break;
                    case "5":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.StoreEditedBy).ToList()
                                                                                                 : data.OrderBy(p => p.StoreEditedBy).ToList();
                        break;

                    default:
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.StoreId).ToList()
                                                                                                 : data.OrderBy(p => p.StoreId).ToList();

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