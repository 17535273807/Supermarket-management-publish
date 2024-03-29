﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 超市管理系统.Entity
{
    internal class CustomerProvider : ProviderBase, IProvider<Customer>
    {
        public static CustomerProvider Current = new CustomerProvider();

        public int Delete(Customer entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            return db.SaveChanges();
        }

        public List<Customer> GetAll()
        {
            return db.Customer.ToList();
        }

        public int Insert(Customer entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Added;
            return db.SaveChanges();
        }

        public int Save()
        {
            return db.SaveChanges();
        }

        public int Update(Customer entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
