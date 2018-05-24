using Model.DAO;
using Model.EntityFramework;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryPro(long idcatepro, int page = 1, int pageSize = 3)
        {
            ViewBag.Category = new ProductCategoryDAO().ViewDetail((int)idcatepro);

            int totalRecord = 0;
            var model = new ProductDAO().ListByCategoryId(idcatepro, ref totalRecord, page, pageSize);
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

            return View(model);
        }





        public ActionResult ProductDetails(long? idpro)
        {
            ViewBag.newproduct = new ProductDAO().ListNewUpdateProduct(5);
            var product = new ProductDAO().ViewDetail((int)idpro.Value);
            if (product.MoreImages != null)
            {
                string[] img = product.MoreImages.Split(',');
                List<string> listimg = new List<string>();
                for (int i = 0; i < img.Length; i++)
                {
                    if (img[i] != string.Empty)
                    {
                        listimg.Add(img[i]);
                    }
                }
                ViewBag.listimage = listimg;
            }
            return View(product);
        }


        public PartialViewResult _ProductCategory(long? idcateit)
        {
            var DAOproduct = new ProductDAO();
            List<Product> listPro = new List<Product>();
            listPro = DAOproduct.ListProductFirstRun(10, idcateit);
            return PartialView(listPro);
        }



        [ChildActionOnly]
        public ActionResult _Ads(long? idc)
        {
            var DAOAds = new AdDAO();
            List<Ad> listAds = DAOAds.ListAll();
            List<Ad> listAd = new List<Ad>();
            if (idc != 0 && idc != 1)
            {
                Ad ad = new Ad();
                ad.Image = listAds[(int)((int)idc - 2)].Image;

                listAd.Add(ad);
            }
            return PartialView(listAd);
        }


        public JsonResult ListProductWithItem(string product)
        {
            product = product.Replace(".", " ").Replace("₫", " ").Replace(" ", "");

            var jsonProduct = new JavaScriptSerializer().Deserialize<List<Product>>(product);
            foreach (var item in jsonProduct)
            {
                item.Images = "/Data/images/636310466030005418_s81o.jpg";
                item.Name = "Samsung-Galaxy-S8";
            }
            ViewBag.datac = jsonProduct;

            return Json(new { status = true });
        }


    }
}