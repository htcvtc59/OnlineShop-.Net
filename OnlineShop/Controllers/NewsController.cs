using Model.DAO;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            CategoryDAO category = new CategoryDAO();
            ContentDAO content = new ContentDAO();
            List<object> listob = new List<object>();
            listob.Add(category.ListAllCategory());
            listob.Add(content.ListAllContent());

            return View(listob);
        }

        public PartialViewResult ListCNews(long idconn)
        {
            ContentDAO content = new ContentDAO();
            List<Content> listCon = content.ListConCategory(idconn);
            return PartialView(listCon);
        }


        public ActionResult DetailNewsContent(long iddetailconn)
        {
            ContentDAO content = new ContentDAO();
            Content cont = content.ViewDetail((int)iddetailconn);
            return View(cont);
        }


    }
}