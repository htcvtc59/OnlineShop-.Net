using Common;
using Model.DAO;
using Model.EntityFramework;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CommonConstants.CART_SESSION];
            var list = new List<CartChild>();
            if (cart != null)
            {
                list = (List<CartChild>)cart;
            }
            return View(list);
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartChild>>(cartModel);
            var sessionCart = (List<CartChild>)Session[CommonConstants.CART_SESSION];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CommonConstants.CART_SESSION] = sessionCart;

            return Json(new { status = true });
        }

        public JsonResult Delete(long? id)
        {
            var sessionCart = (List<CartChild>)Session[CommonConstants.CART_SESSION];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CommonConstants.CART_SESSION] = sessionCart;
            return Json(new { status = true });
        }

        public JsonResult Deleteitem(long? id)
        {
            var sessionCart = (List<CartChild>)Session[CommonConstants.CART_SESSION];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CommonConstants.CART_SESSION] = sessionCart;
            return Json(new { status = true });
        }

        public ActionResult AddProToCart(long? idtocart, int quantity)
        {
            var product = new ProductDAO().ViewDetail((int)idtocart);
            var cart = Session[CommonConstants.CART_SESSION];
            if (cart != null)
            {
                var list = (List<CartChild>)cart;
                if (list.Exists(x => x.Product.ID == idtocart))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == idtocart)
                        {
                            item.Quantity += quantity;
                        }
                    }

                }
                else
                {
                    var item = new CartChild();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[CommonConstants.CART_SESSION] = list;
            }
            else
            {
                var list = new List<CartChild>();
                var item = new CartChild();
                item.Product = product;
                item.Quantity = quantity;
                list.Add(item);

                Session[CommonConstants.CART_SESSION] = list;
            }

            return RedirectToAction("Index", "Home");
        }


        [ChildActionOnly]
        public ActionResult _Cart()
        {
            var cart = Session[CommonConstants.CART_SESSION];
            var list = new List<CartChild>();
            if (cart != null)
            {
                list = (List<CartChild>)cart;
            }
            return PartialView(list);
        }

        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CommonConstants.CART_SESSION];
            var list = new List<CartChild>();
            if (cart != null)
            {
                list = (List<CartChild>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address, string email)
        {
            var order = new OrderBy();
            order.CreatedDate = DateTime.Now;
            order.ShipName = shipName;
            order.ShipMobile = mobile;
            order.ShipEmail = email;
            order.ShipAddress = address;
            try
            {
                decimal total=0;
                var id = new OrderByDAO().Insert(order);
                var cart = (List<CartChild>)Session[CommonConstants.CART_SESSION];
                var detailDAO = new OrderDetailDAO();
                foreach (var item in cart)
                {
                    var orderdetail = new OrderDetail();
                    orderdetail.ProductID = item.Product.ID;
                    orderdetail.OrderID = id;
                    orderdetail.Price = item.Product.Price;
                    orderdetail.Quantity = item.Quantity;
                    orderdetail.Status = true;
                    detailDAO.Insert(orderdetail);

                    total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                }

                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/Client/template/neworder.html"));
                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", mobile);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", string.Format("{0:C}", (total)).Replace(",00", ""));
                

                new MailHelper().SendMail("Đơn hàng từ AppleStore", content);
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("Success", "Cart");
        }


        public ActionResult Success()
        {
            //var cart = (List<CartChild>)Session[CommonConstants.CART_SESSION];
            Session[CommonConstants.CART_SESSION] = null;
            return View();
        }



    }
}