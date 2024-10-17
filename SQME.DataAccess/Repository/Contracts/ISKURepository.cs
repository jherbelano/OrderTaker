using SQME.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQME.DataAccess.Repository.Contracts
{
    public interface ISKURepository : IRepository<SKU>
    {
        void Update(SKU obj);
    }
}
