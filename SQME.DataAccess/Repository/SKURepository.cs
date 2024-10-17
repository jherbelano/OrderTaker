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
    public class SKURepository : Repository<SKU>, ISKURepository
    {
        private readonly ApplicationDbContext _db;

        public SKURepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(SKU obj)
        {
            _db.SKUs.Update(obj);
        }
    }
}
