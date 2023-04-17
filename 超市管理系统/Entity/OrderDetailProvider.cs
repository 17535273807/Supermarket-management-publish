using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 超市管理系统.Entity
{
    public class OrderDetailProvider : ProviderBase, IProvider<OrderDetail>
    {
        public static OrderDetailProvider Current = new OrderDetailProvider();

        public int Delete(OrderDetail entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            return db.SaveChanges();
        }

        public List<OrderDetail> GetAll()
        {
            return db.OrderDetail.ToList();
        }

        public int Insert(OrderDetail entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Added;
            return db.SaveChanges();
        }

        public int Save()
        {
            return db.SaveChanges();
        }

        public int Update(OrderDetail entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
