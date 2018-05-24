using Model.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class OrderDetailDAO
    {
        OnlineShopDbContext db = null;
        public OrderDetailDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(OrderDetail entity)
        {
            db.OrderDetails.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(OrderDetail entity)
        {
            try
            {
                var order = db.OrderDetails.Find(entity.ID);
                order.ProductID = entity.ProductID;
                order.OrderID = entity.OrderID;
                order.Quantity = entity.Quantity;
                order.Price = entity.Price;
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
                var order = db.OrderDetails.Find(id);
                db.OrderDetails.Remove(order);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<OrderDetail> ListAllOrderDetail(string searchString, int page, int pageSize)
        {
            IQueryable<OrderDetail> model = db.OrderDetails;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ProductID.ToString().Contains(searchString) || x.Price.ToString().Contains(searchString));
            }

            //return db.Accounts.OrderByDescending(x => x.LevelUser).ToPagedList(page, pageSize);
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        public OrderDetail ViewDetail(int id)
        {
            return db.OrderDetails.Find(id);
        }


        public bool ChangeStatus(long id)
        {
            var order = db.OrderDetails.Find(id);
            order.Status = !order.Status;
            db.SaveChanges();
            return order.Status;
        }


    }
}
