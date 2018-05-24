using Model.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CategoryDAO
    {
        OnlineShopDbContext db = null;
        public CategoryDAO()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Category entity)
        {
            try
            {
                var proCate = db.Categories.Find(entity.ID);
                proCate.Name = entity.Name;
                proCate.MetaTitle = entity.MetaTitle;
                proCate.ParentID = entity.ParentID;
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
                var proCate = db.Categories.Find(id);
                db.Categories.Remove(proCate);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IEnumerable<Category> ListAllCate(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString)||x.MetaTitle.Contains(searchString)||x.DisplayOrder.ToString().Contains(searchString));
            }

            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        public Category ViewDetail(int id)
        {
            return db.Categories.Find(id);
        }


        public bool ChangeStatus(long id)
        {
            var proCate = db.Categories.Find(id);
            proCate.Status = !proCate.Status;
            db.SaveChanges();
            return proCate.Status;
        }

        public Category GetByID(long id)
        {
            return db.Categories.Find(id);
        }
        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }

        public List<Category> ListAllCategory()
        {
            return db.Categories.Where(x => x.Status == true).OrderBy(x=>x.DisplayOrder).ToList();
        }

    }
}
