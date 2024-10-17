using SQME.DataAccess.Data;
using SQME.DataAccess.Repository.Contracts;
using SQME.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQME.DataAccess.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Customer obj)
        {
            _db.Customers.Update(obj);
        }
    }
}
