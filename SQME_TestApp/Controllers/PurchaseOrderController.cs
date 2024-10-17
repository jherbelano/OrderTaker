using Microsoft.AspNetCore.Mvc;
using SQME.DataAccess.Repository.Contracts;
using SQME.Models;

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
            List<PurchaseOrder> PurchaseOrderList = _unitOfWork.PurchaseOrder.GetAll().ToList();

            return View(PurchaseOrderList);
        }
    }
}
