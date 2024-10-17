using Microsoft.AspNetCore.Mvc;
using SQME.DataAccess.Repository.Contracts;
using SQME.Models;

namespace SQME_TestApp.Controllers
{
    public class SKUController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SKUController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<SKU> SKUList = _unitOfWork.SKU.GetAll().ToList();

            return View(SKUList);
        }

        [HttpPost]
        public IActionResult Upsert(SKU obj)
        {
            if (ModelState.IsValid)
            {
                if (obj == null || obj.ID == 0)
                {
                    obj.DateCreated = DateTime.Now;
                    obj.Timestamp = DateTime.Now;
                    _unitOfWork.SKU.Add(obj);
                    _unitOfWork.Save();
                }
                else
                {
                    obj.Timestamp = DateTime.Now;
                    _unitOfWork.SKU.Update(obj);
                    _unitOfWork.Save();
                }

                return RedirectToAction("Index");
            }

            return PartialView("_Upsert", obj);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var sku = _unitOfWork.SKU.Get(c => c.ID == id);

            if (sku== null)
            {
                return NotFound();
            }

            _unitOfWork.SKU.Remove(sku);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
