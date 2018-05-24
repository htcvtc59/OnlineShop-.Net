using Model.DAO;
using Model.EntityFramework;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginAcc model)
        {
            if (ModelState.IsValid)
            {
                var DAO = new AccountDAO();
                var result = DAO.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var acc = DAO.GetById(model.UserName);
                    var accSession = new AccountSession();
                    accSession.Account = acc;

                    Session.Add(CommonConstants.ACCOUNT_SESSION, accSession);


                    return Redirect("/");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == 2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
            }

            return View("Login");
        }

        public ActionResult LogoutAcc()
        {
            Session[CommonConstants.ACCOUNT_SESSION] = null;
            return Redirect("/");
        }

        [ChildActionOnly]
        [HttpGet]
        public PartialViewResult _Register()
        {
            return PartialView();
        }


        [ChildActionOnly]
        [HttpPost]
        public PartialViewResult _Register(RegisterAcc account)
        {
            if (ModelState.IsValid)
            {
                var DAO = new AccountDAO();
                var acc = new Account();
                acc.Email = account.REmail;
                acc.LevelUser = "client";
                acc.Password = Common.Encryptor.MD5Hash(account.RPassword);
                acc.RealName = account.RRealName;
                acc.Status = true;
                acc.UserName = account.RUserName;
                if (DAO.CheckUser(acc) == 0)
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã có người đăng ký");
                    SetAlert("Đăng ký không thành công", "success");
                }
                else if (DAO.CheckUser(acc) == 1)
                {
                    ModelState.AddModelError("", "Email đã có người đăng ký");
                    SetAlert("Đăng ký không thành công", "success");
                }
                else if (DAO.CheckUser(acc) == 2)
                {
                    DAO.Insert(acc);

                    SetAlert("Đăng ký thành công", "success");

                }

            }else
            {
                SetAlert("Đăng ký không thành công", "success");
            }
           
            return PartialView();
        }

        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }

        }

    }
}