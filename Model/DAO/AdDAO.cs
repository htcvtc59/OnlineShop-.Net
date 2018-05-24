using Model.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class AdDAO
    {
        OnlineShopDbContext db = null;
        public AdDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(Ad entity)
        {
            db.Ads.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Ad entity)
        {
            try
            {
                var ad = db.Ads.Find(entity.ID);
                ad.Image = entity.Image;
                ad.Link = entity.Link;
                ad.Description = entity.Description;
                ad.DisplayOrder = entity.DisplayOrder;
                ad.Status = entity.Status;
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
                var slide = db.Ads.Find(id);
                db.Ads.Remove(slide);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Ad> ListAllAd(string searchString, int page, int pageSize)
        {
            IQueryable<Ad> model = db.Ads;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Image.Contains(searchString) || x.Link.Contains(searchString));
            }

            //return db.Accounts.OrderByDescending(x => x.LevelUser).ToPagedList(page, pageSize);
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        public Ad ViewDetail(int id)
        {
            return db.Ads.Find(id);
        }


        public bool ChangeStatus(long id)
        {
            var ad = db.Ads.Find(id);
            ad.Status = !ad.Status;
            db.SaveChanges();
            return ad.Status;
        }

        public List<Ad> ListAll()
        {
            return db.Ads.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
