using Model.DAO;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        // GET: Admin/Slide
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var DAO = new SlideDAO();
            var model = DAO.ListAllSlide(searchString, page, pageSize);
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
            var slide = new SlideDAO().ViewDetail(id);
            return View(slide);
        }

        [HttpPost]
        public ActionResult Create(Slide menu)
        {
            if (ModelState.IsValid)
            {
                var DAO = new SlideDAO();
                long id = DAO.Insert(menu);
                if (id > 0)
                {
                    SetAlert("Thêm thành công", "success");
                    return RedirectToAction("Index", "Slide");
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
        public ActionResult Edit(Slide slide)
        {
            if (ModelState.IsValid)
            {
                var DAO = new SlideDAO();
                var result = DAO.Update(slide);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "Slide");
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
            new SlideDAO().Delete(id);
            SetAlert("Xóa thành công", "success");
            return RedirectToAction("Index", "Slide");
        }


        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new SlideDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }


    }
}