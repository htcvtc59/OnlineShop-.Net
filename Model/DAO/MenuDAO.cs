using Model.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class MenuDAO
    {
        OnlineShopDbContext db = null;
        public MenuDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(Menu entity)
        {
            db.Menus.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Menu entity)
        {
            try
            {
                var menu = db.Menus.Find(entity.ID);
                menu.Text = entity.Text;
                menu.Link = entity.Link;
                menu.DisplayOrder = entity.DisplayOrder;
                menu.Target = entity.Target;
                menu.TypeID = entity.TypeID;
                menu.Status = entity.Status;
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
                var menu = db.Menus.Find(id);
                db.Menus.Remove(menu);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Menu> ListAllMenu(string searchString, int page, int pageSize)
        {
            IQueryable<Menu> model = db.Menus;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Text.Contains(searchString));
            }

            //return db.Accounts.OrderByDescending(x => x.LevelUser).ToPagedList(page, pageSize);
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        public Menu ViewDetail(int id)
        {
            return db.Menus.Find(id);
        }


        public bool ChangeStatus(long id)
        {
            var user = db.Menus.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }

        public List<Menu> ListAll()
        {
            return db.Menus.Where(x => x.Status == true).OrderBy(x=>x.DisplayOrder).ToList();
        }

    }
}
