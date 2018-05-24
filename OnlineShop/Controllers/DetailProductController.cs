using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class DetailProductController : Controller
    {
        // GET: DetailProduct
        public ActionResult Index()
        {
            return View();
        }


        [ChildActionOnly]
        public ActionResult _ConfigDesAjax(long idconfig)
        {
            var product = new ProductDAO().ViewDetail((int)idconfig);


            return PartialView(product);
        }

        [ChildActionOnly]
        public ActionResult _DesDetailAjax(long iddes)
        {
            var product = new ProductDAO().ViewDetail((int)iddes);

            return PartialView(product);
        }






    }
}