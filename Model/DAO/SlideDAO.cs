using Model.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class SlideDAO
    {
        OnlineShopDbContext db = null;
        public SlideDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(Slide entity)
        {
            db.Slides.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Slide entity)
        {
            try
            {
                var slide = db.Slides.Find(entity.ID);
                slide.Image = entity.Image;
                slide.Link = entity.Link;
                slide.Description = entity.Description;
                slide.DisplayOrder = entity.DisplayOrder;
                slide.Status = entity.Status;
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
                var slide = db.Slides.Find(id);
                db.Slides.Remove(slide);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Slide> ListAllSlide(string searchString, int page, int pageSize)
        {
            IQueryable<Slide> model = db.Slides;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Image.Contains(searchString)||x.Link.Contains(searchString));
            }

            //return db.Accounts.OrderByDescending(x => x.LevelUser).ToPagedList(page, pageSize);
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        public Slide ViewDetail(int id)
        {
            return db.Slides.Find(id);
        }


        public bool ChangeStatus(long id)
        {
            var user = db.Slides.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }

        public List<Slide> ListAll()
        {
            return db.Slides.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

    }
}
