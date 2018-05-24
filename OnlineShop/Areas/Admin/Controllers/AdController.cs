using Model.DAO;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class AdController : BaseController
    {
        // GET: Admin/Ad
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var DAO = new AdDAO();
            var model = DAO.ListAllAd(searchString, page, pageSize);
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
            var slide = new AdDAO().ViewDetail(id);
            return View(slide);
        }

        [HttpPost]
        public ActionResult Create(Ad ads)
        {
            if (ModelState.IsValid)
            {
                var DAO = new AdDAO();
                long id = DAO.Insert(ads);
                if (id > 0)
                {
                    SetAlert("Thêm thành công", "success");
                    return RedirectToAction("Index", "Ad");
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
        public ActionResult Edit(Ad ads)
        {
            if (ModelState.IsValid)
            {
                var DAO = new AdDAO();
                var result = DAO.Update(ads);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "Ad");
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
            new AdDAO().Delete(id);
            SetAlert("Xóa thành công", "success");
            return RedirectToAction("Index", "Ad");
        }


        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new AdDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}