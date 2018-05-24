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
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var DAO = new ContentDAO();
            var model = DAO.ListAllContent(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Content content)
        {
            if (ModelState.IsValid)
            {
                var DAO = new ContentDAO();
                content.MetaTitle = ConvertToUnSign.convertunsign(content.MetaTitle);
                content.CreatedDate = DateTime.Now;
                long id = DAO.Insert(content);
                if (id > 0)
                {
                    SetAlert("Thêm thành công", "success");
                    return RedirectToAction("Index", "Content");
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
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var DAO = new ContentDAO();
            var proCate = DAO.ViewDetail((int)id);
            var content = DAO.GetByID(id);
            SetViewBag(content.CategoryID);
            return View(proCate);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Content content)
        {
            if (ModelState.IsValid)
            {
                var DAO = new ContentDAO();
                content.MetaTitle = ConvertToUnSign.convertunsign(content.MetaTitle);
                var result = DAO.Update(content);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    SetAlert("Cập nhật thất bại", "error");
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }
            }
            SetViewBag(content.CategoryID);
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ContentDAO().Delete(id);
            SetAlert("Xóa thành công", "success");
            return RedirectToAction("Index", "Content");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ContentDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        public void SetViewBag(long? selectedId = null)
        {
            var DAO = new CategoryDAO();
            ViewBag.CategoryID = new SelectList(DAO.ListAll(), "ID", "Name", selectedId);
        }

    }
}