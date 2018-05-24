using Model.DAO;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class OrderDetailController : BaseController
    {
        // GET: Admin/OrderDetail
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var DAO = new OrderDetailDAO();
            var model = DAO.ListAllOrderDetail(searchString, page, pageSize);
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
            var order = new OrderDetailDAO().ViewDetail(id);
            return View(order);
        }

        [HttpPost]
        public ActionResult Create(OrderDetail order)
        {
            if (ModelState.IsValid)
            {
                var DAO = new OrderDetailDAO();
                long id = DAO.Insert(order);
                if (id > 0)
                {
                    SetAlert("Thêm thành công", "success");
                    return RedirectToAction("Index", "OrderDetail");
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
        public ActionResult Edit(OrderDetail order)
        {
            if (ModelState.IsValid)
            {
                var DAO = new OrderDetailDAO();
                var result = DAO.Update(order);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "OrderDetail");
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
            new OrderDetailDAO().Delete(id);
            SetAlert("Xóa thành công", "success");
            return RedirectToAction("Index", "OrderDetail");
        }


        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new OrderDetailDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }


    }
}