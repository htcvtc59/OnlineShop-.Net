using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.DAO
{
    public class UserDAO
    {
        OnlineShopDbContext db = null;
        public UserDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(Account entity)
        {
            db.Accounts.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Account entity)
        {
            try
            {
                var user = db.Accounts.Find(entity.ID);
                user.RealName = entity.RealName;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Email = entity.Email;
                user.LevelUser = entity.LevelUser;
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
                var user = db.Accounts.Find(id);
                db.Accounts.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Account> ListAllAccount(string searchString, int page, int pageSize)
        {
            IQueryable<Account> model = db.Accounts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.RealName.Contains(searchString));
            }

            //return db.Accounts.OrderByDescending(x => x.LevelUser).ToPagedList(page, pageSize);
            return model.OrderBy(x => x.LevelUser).ToPagedList(page, pageSize);
        }
        public int LoginAdmin(string user, string pass)
        {
            var result = db.Accounts.SingleOrDefault(x => x.UserName == user && x.LevelUser == "admin");
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Password == pass)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
        }
        public Account GetById(string userName)
        {
            return db.Accounts.SingleOrDefault(x => x.UserName == userName);
        }

        public Account ViewDetail(int id)
        {
            return db.Accounts.Find(id);
        }
        public bool Login(string user, string pass)
        {
            var result = db.Accounts.Count(x => x.UserName == user && x.Password == pass);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ChangeStatus(long id)
        {
            var user = db.Accounts.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }


    }
}
