using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class AccountDAO
    {
        OnlineShopDbContext db = null;
        public AccountDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(Account entity)
        {
            db.Accounts.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public int CheckUser(Account entity)
        {
            int acc = db.Accounts.Count(x => x.UserName == entity.UserName);
            if (acc > 0)
            {
                return 0;
            }
            else
            {
                int acce = db.Accounts.Count(x => x.Email == entity.Email);
                if (acce > 0)
                    return 1;
                else
                    return 2;
            }
        }

        public int Login(string user, string pass)
        {
            var result = db.Accounts.SingleOrDefault(x => x.UserName == user && x.LevelUser == "client");
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

    }
}
