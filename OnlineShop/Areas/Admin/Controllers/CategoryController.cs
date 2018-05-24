using Model.DAO;
using Model.EntityFramework;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var DAO = new CategoryDAO();
            var model = DAO.ListAllCate(searchString, page, pageSize);
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
            var proCate = new CategoryDAO().ViewDetail(id);
            return View(proCate);
        }

        [HttpPost]
        public ActionResult Create(Category proCate)
        {
            if (ModelState.IsValid)
            {
                var DAO = new CategoryDAO();
                proCate.MetaTitle = ConvertToUnSign.convertunsign(proCate.MetaTitle);
                proCate.CreatedDate = DateTime.Now;
                long id = DAO.Insert(proCate);
                if (id > 0)
                {
                    SetAlert("Thêm thành công", "success");
                    return RedirectToAction("Index", "Category");
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
        public ActionResult Edit(Category proCate)
        {
            if (ModelState.IsValid)
            {
                var DAO = new CategoryDAO();
                proCate.MetaTitle = ConvertToUnSign.convertunsign(proCate.MetaTitle);
                var result = DAO.Update(proCate);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "Category");
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
            new CategoryDAO().Delete(id);
            SetAlert("Xóa thành công", "success");
            return RedirectToAction("Index", "Category");
        }


        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new CategoryDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

       
    }
}