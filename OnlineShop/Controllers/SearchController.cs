using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }


        public PartialViewResult SearchListPro(string keyword, string dropKey, int page = 1, int pageSize = 18)
        {
            //ViewBag.Category = new ProductCategoryDAO().ViewDetail((int)idcatepro);

            int totalRecord = 0;
            var model = new ProductDAO().ListAllSearch(keyword, dropKey, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            int maxPage = 5;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return PartialView(model);
        }
        

        public ActionResult _Search()
        {
            return PartialView();
        }
    }
}