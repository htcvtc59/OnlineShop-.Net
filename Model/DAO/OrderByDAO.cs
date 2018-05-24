using Model.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
   public class OrderByDAO
    {
        OnlineShopDbContext db = null;
        public OrderByDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(OrderBy entity)
        {
            db.OrderBies.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(OrderBy entity)
        {
            try
            {
                var order = db.OrderBies.Find(entity.ID);
                order.CreatedDate = entity.CreatedDate;
                order.CustomerID = entity.CustomerID;
                order.ShipName = entity.ShipName;
                order.ShipMobile = entity.ShipMobile;
                order.ShipAddress = entity.ShipAddress;
                order.ShipEmail = entity.ShipEmail;
                order.Status = entity.Status;
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
                var order = db.OrderBies.Find(id);
                db.OrderBies.Remove(order);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<OrderBy> ListAllOrderBy(string searchString, int page, int pageSize)
        {
            IQueryable<OrderBy> model = db.OrderBies;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ShipName.Contains(searchString)||x.ShipAddress.Contains(searchString)||x.ShipEmail.Contains(searchString));
            }

            //return db.Accounts.OrderByDescending(x => x.LevelUser).ToPagedList(page, pageSize);
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        public OrderBy ViewDetail(int id)
        {
            return db.OrderBies.Find(id);
        }


        public bool ChangeStatus(long id)
        {
            var order = db.OrderBies.Find(id);
            order.Status = !order.Status;
            db.SaveChanges();
            return order.Status;
        }
    }
}
