using Model.DAO;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class OrderByController : BaseController
    {
        // GET: Admin/OrderBy
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var DAO = new OrderByDAO();
            var model = DAO.ListAllOrderBy(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var order = new OrderByDAO().ViewDetail(id);
            return View(order);
        }

        [HttpPost]
        public ActionResult Create(OrderBy order)
        {
            if (ModelState.IsValid)
            {
                var DAO = new OrderByDAO();
                long id = DAO.Insert(order);
                if (id > 0)
                {
                    SetAlert("Thêm thành công", "success");
                    return RedirectToAction("Index", "OrderBy");
                }
                else
                {
                    SetAlert("Thêm thất bại", "error");
                    ModelState.AddModelError("", "Thêm thất bại");
                }
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult Edit(OrderBy order)
        {
            if (ModelState.IsValid)
            {
                var DAO = new OrderByDAO();
                var result = DAO.Update(order);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "OrderBy");
                }
                else
                {
                    SetAlert("Cập nhật thất bại", "error");
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }
            }

            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new OrderByDAO().Delete(id);
            SetAlert("Xóa thành công", "success");
            return RedirectToAction("Index", "OrderBy");
        }


        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new OrderByDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }



    }
}