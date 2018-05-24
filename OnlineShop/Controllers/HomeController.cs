using Model.DAO;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.slides = new SlideDAO().ListAll();
            ViewBag.newproduct = new ProductDAO().ListNewUpdateProduct(5);
            ViewBag.popularpro = new ProductDAO().ListPopularProduct(5);
            ViewBag.newscon = new ContentDAO().ListAllContent();

            var DAOmenu = new MenuDAO();
            var DAOprocate = new ProductCategoryDAO();
            List<Menu> listMenu = DAOmenu.ListAll();
            listMenu.RemoveAt(DAOmenu.ListAll().Count - 1);
            List<object> myModel = new List<object>();
            myModel.Add(listMenu);
            myModel.Add(DAOprocate.ListAll());
            return View(myModel);
        }

        [ChildActionOnly]
        public ActionResult _Menu()
        {
            var DAOmenu = new MenuDAO();
            var DAOprocate = new ProductCategoryDAO();
            List<object> myModel = new List<object>();
            myModel.Add(DAOmenu.ListAll());
            myModel.Add(DAOprocate.ListAll());
            return PartialView(myModel);
        }



        public PartialViewResult ProductWithItem(long? idcatei)
        {
            var DAOproduct = new ProductDAO();
            List<Product> listPro = new List<Product>();
            listPro = DAOproduct.ListProduct(10, idcatei);
            return PartialView(listPro);
        }


    }
}