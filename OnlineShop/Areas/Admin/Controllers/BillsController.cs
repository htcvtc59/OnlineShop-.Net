using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BillsController : BaseController
    {
        // GET: Admin/Bills
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            var DAO = new BillsDAO();
            var model = DAO.ListAllBills(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new BillsDAO().Delete(id);
            SetAlert("Xóa thành công", "success");
            return RedirectToAction("Index", "Bills");
        }

        public ActionResult ViewDetail(int idbills)
        {
            var model = new BillsDAO().ViewDetail(idbills);
            return PartialView("ViewDetail", model);
        }

        


    }
}