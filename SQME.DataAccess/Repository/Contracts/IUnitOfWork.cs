using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQME.DataAccess.Repository.Contracts
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customer { get; }
        ISKURepository SKU { get; }
        IPurchaseOrderRepository PurchaseOrder { get; }

        void Save();
    }
}
