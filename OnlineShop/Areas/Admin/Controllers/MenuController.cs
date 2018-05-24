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
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var DAO = new MenuDAO();
            var model = DAO.ListAllMenu(searchString, page, pageSize);
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
            var menu = new MenuDAO().ViewDetail(id);
            return View(menu);
        }

        [HttpPost]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                var DAO = new MenuDAO();
                menu.Link = ConvertToUnSign.convertunsign(menu.Link);
                long id = DAO.Insert(menu);
                if (id > 0)
                {
                    SetAlert("Thêm thành công", "success");
                    return RedirectToAction("Index", "Menu");
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
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                var DAO = new MenuDAO();
                menu.Link = ConvertToUnSign.convertunsign(menu.Link);
                var result = DAO.Update(menu);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "Menu");
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
            new MenuDAO().Delete(id);
            SetAlert("Xóa thành công", "success");
            return RedirectToAction("Index", "Menu");
        }


        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new MenuDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}