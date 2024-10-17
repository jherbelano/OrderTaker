using SQME.DataAccess.Data;
using SQME.DataAccess.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQME.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public ICustomerRepository Customer { get; private set; }
        public ISKURepository SKU { get; private set; }
        public IPurchaseOrderRepository PurchaseOrder { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Customer = new CustomerRepository(_db);
            SKU = new SKURepository(_db);
            PurchaseOrder = new PurchaseOrderRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
