using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SQME.DataAccess.Repository.Contracts;
using SQME.Models;
using SQME.Models.ViewModel;

namespace SQME_TestApp.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseOrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<PurchaseOrder> PurchaseOrderList = _unitOfWork.PurchaseOrder.GetAll(includeProperties: "Customer").ToList();

            return View(PurchaseOrderList);
        }

        public IActionResult Upsert(int? id)
        {
            PurchaseOrderVM purchaseOrderVM = new()
            {
                CustomerList = _unitOfWork.Customer.GetAll().Select(c => new SelectListItem
                {
                    Text = c.FullName,
                    Value = c.ID.ToString(),
                }),
                SKUList = _unitOfWork.SKU.GetAll().Select(s => new SelectListItem
                {
                    Text = $"{s.Code} - {s.Name}",
                    Value = s.ID.ToString(),                    
                }),
                StatusList = Enum.GetValues(typeof(OrderStatus))
                                .Cast<OrderStatus>()
                                .Select(s => new SelectListItem
                                {
                                    Value = ((int)s).ToString(),
                                    Text = s.ToString()
                                }).ToList(),
                PurchaseOrder = new PurchaseOrder()
            };

            if (id == null || id == 0)
            {
                purchaseOrderVM.PurchaseOrder.DateOfDelivery = DateTime.Now.AddDays(2);
                return View(purchaseOrderVM);
            }
            else
            {
                purchaseOrderVM.PurchaseOrder = _unitOfWork.PurchaseOrder.Get(p => p.ID == id, includeProperties: "PurchaseOrderItems") ?? new PurchaseOrder();
                if (purchaseOrderVM.PurchaseOrder == null)
                {
                    return NotFound();
                }

            }
            return View(purchaseOrderVM);
        }

        [HttpPost]
        public IActionResult Upsert(PurchaseOrderVM obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.PurchaseOrder == null || obj.PurchaseOrder.ID == 0)
                {
                    obj.PurchaseOrder.DateCreated = DateTime.Now;
                    obj.PurchaseOrder.Timestamp = DateTime.Now;
                    _unitOfWork.PurchaseOrder.Add(obj.PurchaseOrder);
                    _unitOfWork.Save();
                }
                else
                {
                    obj.PurchaseOrder.Timestamp = DateTime.Now;
                    _unitOfWork.PurchaseOrder.Update(obj.PurchaseOrder);
                    _unitOfWork.Save();
                }
               
                return RedirectToAction("Index");
            }
            obj.CustomerList = _unitOfWork.Customer.GetAll().Select(c => new SelectListItem
            {
                Text = c.FullName,
                Value = c.ID.ToString(),
            });
            obj.SKUList = _unitOfWork.SKU.GetAll().Select(s => new SelectListItem
            {
                Text = $"{s.Code} - {s.Name}",
                Value = s.ID.ToString(),
            });
            obj.StatusList = Enum.GetValues(typeof(OrderStatus))
                                .Cast<OrderStatus>()
                                .Select(s => new SelectListItem
                                {
                                    Value = ((int)s).ToString(),
                                    Text = s.ToString()
                                }).ToList();
            return View(obj);
        }

        public IActionResult GetUnitPrice(int skuId)
        {
            var sku = _unitOfWork.SKU.Get(s => s.ID == skuId);
            if (sku == null)
            {
                return NotFound();
            }
            return Json(new { unitPrice = sku.UnitPrice });
        }
    }
}
