using Model.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Model.DAO
{
    public class ProductDAO
    {
        OnlineShopDbContext db = null;
        public ProductDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Product entity)
        {
            try
            {
                var product = db.Products.Find(entity.ID);
                product.Name = entity.Name;
                product.Code = entity.Code;
                product.MetaTitle = entity.MetaTitle;
                product.Description = entity.Description;
                product.Images = entity.Images;
                product.MoreImages = entity.MoreImages;
                product.Price = entity.Price;
                product.Quantity = entity.Quantity;
                product.CategoryID = entity.CategoryID;
                product.Detail = entity.Detail;
                product.Warranty = entity.Warranty;
                product.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Delete(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Product> ListAllProduct(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Code.Contains(searchString)
                || x.Detail.Contains(searchString) || x.Description.Contains(searchString)
                || x.CategoryID.HasValue.ToString().Contains(searchString));
            }

            //return db.Accounts.OrderByDescending(x => x.LevelUser).ToPagedList(page, pageSize);
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }


        public bool ChangeStatus(long id)
        {
            var user = db.Products.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }

        //San pham moi cap nhat
        public List<Product> ListNewUpdateProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.ID).Take(top).ToList();
        }

        //San pham ban chay
        public List<Product> ListPopularProduct(int top)
        {
            IQueryable<Product> model = db.Products;
            IQueryable<OrderDetail> modelo = db.OrderDetails;

            var resultO = db.OrderDetails.GroupBy(x => x.ProductID).Select(z => new { Id = z.Key, Count = z.Count() });
            var resultP = resultO.Join(model, z => z.Id, p => p.ID,
                (z, p) => new { product = p, count = z.Count }).OrderByDescending(x => x.count).Select(y => y.product).Take(top).ToList();

            if (resultP != null)
            {
                return resultP;
            }
            else
            {
                return model.OrderByDescending(x => x.Quantity).Take(top).ToList();
            }

        }


        public List<Product> ListProWithCateID(int top, long? idcate)
        {
            return db.Products.Where(x => x.CategoryID == idcate).Take(top).ToList();
        }

        public List<Product> ListProductFirstRun(int top, long? idprocate)
        {
            IQueryable<Product> model = db.Products;
            List<ProductCategory> lstprocate = db.ProductCategories.Where(x => x.ParentID == idprocate).OrderBy(x => x.ID).Take(1).ToList();
            foreach (var item in lstprocate)
            {
                model = model.Where(x => x.CategoryID == item.ID);

            }

            return model.OrderBy(x => x.ID).Take(top).ToList();

        }

        public List<Product> ListProduct(int top, long? idprocate)
        {
            IQueryable<Product> model = db.Products;
            model = model.Where(x => x.CategoryID == idprocate);

            return model.OrderBy(x => x.ID).Take(top).ToList();

        }


        public List<Product> ListByCategoryId(long cate, ref int totalRecord, int pageIndex, int pageSize)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == cate).Count();
            var model = db.Products.Where(x => x.CategoryID == cate).OrderBy(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return model;
        }

        public List<Product> ListAllSearch(string keyword, string moneyorkey, ref int totalRecord, int pageIndex, int pageSize)
        {
            List<Product> model = new List<Product>();
            if (moneyorkey.Equals("0"))
            {
                totalRecord = db.Products.Where(x => x.Price.ToString().Contains(keyword)).Count();
                model = db.Products.Where(x => x.Price.ToString().Contains(keyword)).OrderBy(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return model;
            }
            else if(moneyorkey.Equals("1"))
            {
                totalRecord = db.Products.Where(x => x.Name.Contains(keyword)).Count();
                model = db.Products.Where(x => x.Name.Contains(keyword)).OrderBy(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return model;
            }
            else
            {
                return null;
            }
            
        }


    }
}
