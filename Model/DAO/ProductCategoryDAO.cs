using Model.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ProductCategoryDAO
    {
        OnlineShopDbContext db = null;
        public ProductCategoryDAO()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(ProductCategory entity)
        {
            db.ProductCategories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(ProductCategory entity)
        {
            try
            {
                var proCate = db.ProductCategories.Find(entity.ID);
                proCate.Name = entity.Name;
                proCate.ParentID = entity.ParentID;
                proCate.MetaTitle = entity.MetaTitle;
                proCate.DisplayOrder = entity.DisplayOrder;
                proCate.Status = entity.Status;
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
                var proCate = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(proCate);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IEnumerable<ProductCategory> ListAllProCate(string searchString, int page, int pageSize)
        {
            IQueryable<ProductCategory> model = db.ProductCategories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }

            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        public ProductCategory ViewDetail(int id)
        {
            return db.ProductCategories.Find(id);
        }


        public bool ChangeStatus(long id)
        {
            var proCate = db.ProductCategories.Find(id);
            proCate.Status = !proCate.Status;
            db.SaveChanges();
            return proCate.Status;
        }

        public ProductCategory GetByID(long id)
        {
            return db.ProductCategories.Find(id);
        }


        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(x => x.Status == true).OrderBy(x=>x.DisplayOrder).ToList();
        }

        public List<ProductCategory> ListAllByID(long id)
        {
            IQueryable<ProductCategory> model = db.ProductCategories;
            model = model.Where(x => x.Status == true && x.ID == id).OrderBy(x=>x.DisplayOrder);
            return model.ToList();
        }
        

    }
}
