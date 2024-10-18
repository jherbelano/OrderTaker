using SQME.DataAccess.Data;
using SQME.DataAccess.Repository.Contracts;
using SQME.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQME.DataAccess.Repository
{
    public class PurchaseOrderRepository : Repository<PurchaseOrder>, IPurchaseOrderRepository
    {
        private readonly ApplicationDbContext _db;

        public PurchaseOrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PurchaseOrder obj)
        {
            var poToUpdate = _db.PurchaseOrders.Include(p => p.PurchaseOrderItems).SingleOrDefault(p => p.ID == obj.ID);

            if (poToUpdate != null)
            {
                poToUpdate.CustomerID = obj.CustomerID;
                poToUpdate.DateOfDelivery = obj.DateOfDelivery;
                poToUpdate.Status = obj.Status;
                poToUpdate.AmountDue = obj.AmountDue;
                poToUpdate.Timestamp = DateTime.Now;
                poToUpdate.IsActive = obj.IsActive;

                var itemsToRemove = poToUpdate.PurchaseOrderItems
                    .Where(existingItem => !obj.PurchaseOrderItems.Any(updatedItem => updatedItem.ID == existingItem.ID))
                    .ToList();

                foreach (var item in itemsToRemove)
                {
                    poToUpdate.PurchaseOrderItems.Remove(item);
                    _db.PurchaseOrderItems.Remove(item);
                }

                // Add or update items
                foreach (var item in obj.PurchaseOrderItems)
                {
                    var existingItem = poToUpdate.PurchaseOrderItems.FirstOrDefault(i => i.ID == item.ID);
                    if (existingItem == null)
                    {
                        poToUpdate.PurchaseOrderItems.Add(item);
                        _db.SaveChanges();
                    }
                    else
                    {
                        existingItem.SKUID = item.SKUID;
                        existingItem.Quantity = item.Quantity;
                        existingItem.Timestamp = DateTime.Now;
                    }
                }
            }
        }
    }
}
