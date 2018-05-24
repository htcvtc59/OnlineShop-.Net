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
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 6)
        {
            var DAO = new ProductDAO();
            var model = DAO.ListAllProduct(searchString, page, pageSize);
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
            var product = new ProductDAO().ViewDetail(id);
            SetViewBag(product.CategoryID);
            return View(product);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var DAO = new ProductDAO();
                product.MetaTitle = ConvertToUnSign.convertunsign(product.MetaTitle);
                long id = DAO.Insert(product);
                if (id > 0)
                {
                    SetAlert("Thêm thành công", "success");
                    return RedirectToAction("Index", "Product");
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
        [ValidateInput(false)]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var DAO = new ProductDAO();
                product.MetaTitle = ConvertToUnSign.convertunsign(product.MetaTitle);
                var result = DAO.Update(product);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    SetAlert("Cập nhật thất bại", "error");
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }
            }
            SetViewBag(product.CategoryID);
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDAO().Delete(id);
            SetAlert("Xóa thành công", "success");
            return RedirectToAction("Index", "Product");
        }


        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }


        public void SetViewBag(long? selectedId = null)
        {
            var DAO = new ProductCategoryDAO();
            ViewBag.ProductCategoryID = new SelectList(DAO.ListAll(), "ID", "Name", selectedId);
        }
        

    }
}