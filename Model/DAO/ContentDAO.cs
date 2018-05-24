using Model.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ContentDAO
    {
        OnlineShopDbContext db = null;
        public ContentDAO()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(Content entity)
        {
            db.Contents.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Content entity)
        {
            try
            {
                var proCate = db.Contents.Find(entity.ID);
                proCate.Name = entity.Name;
                proCate.MetaTitle = entity.MetaTitle;
                proCate.Description = entity.Description;
                proCate.Images = entity.Images;
                proCate.CategoryID = entity.CategoryID;
                proCate.Detail = entity.Detail;
                proCate.MetaDescriptions = entity.MetaDescriptions;
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
                var proCate = db.Contents.Find(id);
                db.Contents.Remove(proCate);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IEnumerable<Content> ListAllContent(string searchString, int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Detail.Contains(searchString) || x.Description.Contains(searchString));
            }

            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        public Content ViewDetail(int id)
        {
            return db.Contents.Find(id);
        }


        public bool ChangeStatus(long id)
        {
            var proCate = db.Contents.Find(id);
            proCate.Status = !proCate.Status;
            db.SaveChanges();
            return proCate.Status;
        }

        public Content GetByID(long id)
        {
            return db.Contents.Find(id);
        }
        
        public List<Content> ListAllContent()
        {
            return db.Contents.Where(x => x.Status == true).OrderByDescending(x => x.ID).Take(6).ToList();
        }

        public List<Content> ListConCategory(long id)
        {
            return db.Contents.Where(x => x.Status == true&&x.CategoryID==id).OrderBy(x => x.ID).ToList();
        }
        

    }
}
