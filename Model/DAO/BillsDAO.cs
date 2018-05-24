using Model.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class BillsDAO
    {
        OnlineShopDbContext db = null;
        public BillsDAO()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<OrderBy> ListAllBills(string searchString, int page, int pageSize)
        {
            IQueryable<OrderBy> model = db.OrderBies;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ShipName.Contains(searchString) || x.ShipAddress.Contains(searchString) || x.ShipEmail.Contains(searchString));
            }

            //return db.Accounts.OrderByDescending(x => x.LevelUser).ToPagedList(page, pageSize);
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }

        public List<Bills> ViewDetail(int id)
        {
            var result = from d in db.OrderDetails
                         join p in db.Products
on d.ProductID equals p.ID
                         where d.OrderID == id
                         select new Bills()
                         {
                             ID = d.ID,
                             ProductID = d.ProductID,
                             Name = p.Name,
                             Quantity = d.Quantity,
                             Price = d.Price,
                             Status = d.Status

                         };


            return result.Where(x => x.Status == true).OrderBy(x => x.ID).ToList();
        }

        public bool Delete(int id)
        {
            try
            {
                var order = db.OrderDetails.Find(id);
                List<Bills> bills = new BillsDAO().ViewDetail(id);
                Bills bill = bills.Find(x => x.ID == id);
                bills.Remove(bill);
                db.OrderDetails.Remove(order);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        

    }
}
