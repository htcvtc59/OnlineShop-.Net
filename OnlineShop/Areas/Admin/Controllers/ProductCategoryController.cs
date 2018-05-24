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
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var DAO = new ProductCategoryDAO();
            var model = DAO.ListAllProCate(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        public ActionResult Edit(int id)
        {
            var proCate = new ProductCategoryDAO().ViewDetail(id);
            
            SetViewBag(proCate.ParentID);
            return View(proCate);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory proCate)
        {
            if (ModelState.IsValid)
            {
                var DAO = new ProductCategoryDAO();
                proCate.MetaTitle = ConvertToUnSign.convertunsign(proCate.MetaTitle);
                long id = DAO.Insert(proCate);
                if (id > 0)
                {
                    SetAlert("Thêm thành công", "success");
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    SetAlert("Thêm thất bại", "error");
                    ModelState.AddModelError("", "Thêm thất bại");
                }
            }
            SetViewBag();
            return View("Index");
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory proCate)
        {
            if (ModelState.IsValid)
            {
                var DAO = new ProductCategoryDAO();
                proCate.MetaTitle = ConvertToUnSign.convertunsign(proCate.MetaTitle);
                var result = DAO.Update(proCate);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    SetAlert("Cập nhật thất bại", "error");
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }
            }

            SetViewBag(proCate.ParentID);
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductCategoryDAO().Delete(id);
            SetAlert("Xóa thành công", "success");
            return RedirectToAction("Index", "ProductCategory");
        }


        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductCategoryDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        public void SetViewBag(long? selectedId = null)
        {
            var DAO = new MenuDAO();
            ViewBag.ParentID = new SelectList(DAO.ListAll(), "ID", "Text", selectedId);
        }


    }
}