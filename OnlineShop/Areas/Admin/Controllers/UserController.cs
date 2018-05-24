using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EntityFramework;
using Model.DAO;
using OnlineShop.Common;
using PagedList.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page=1,int pageSize=10)
        {
            var DAO = new UserDAO();
            var model = DAO.ListAllAccount(searchString, page, pageSize);
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
            var user = new UserDAO().ViewDetail(id);
            return View(user);
        }


        [HttpPost]
        public ActionResult Create(Account user)
        {
            if (ModelState.IsValid)
            {
                var DAO = new UserDAO();
                user.Password = Encryptor.MD5Hash(user.Password);
                long id = DAO.Insert(user);
                if (id > 0)
                {
                    SetAlert("Thêm tài khoản thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Thêm tài khoản thất bại", "error");
                    ModelState.AddModelError("", "Thêm tài khoản thất bại");
                }
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult Edit(Account user)
        {
            if (ModelState.IsValid)
            {
                var DAO = new UserDAO();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    user.Password = Encryptor.MD5Hash(user.Password);
                }
                var result = DAO.Update(user);
                if (result)
                {
                    SetAlert("Cập nhật tài khoản thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Cập nhật tài khoản thất bại", "error");
                    ModelState.AddModelError("","Cập nhật tài khoản thất bại");
                }
            }

            return View("Index");
        }

       [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDAO().Delete(id);
            SetAlert("Xóa tài khoản thành công", "success");
            return RedirectToAction("Index","User");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDAO().ChangeStatus(id);
            return Json(new {
                status = result
            });
        }


    }
}