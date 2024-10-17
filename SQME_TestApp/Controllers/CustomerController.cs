using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SQME.DataAccess.Repository.Contracts;
using SQME.Models;

namespace SQME_TestApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Customer> CustomerList = _unitOfWork.Customer.GetAll().ToList();

            return View(CustomerList);
        }

        [HttpPost]
        public IActionResult Upsert(Customer obj)
        {
            if (ModelState.IsValid)
            {
                if (obj== null || obj.ID == 0)
                {
                    obj.DateCreated = DateTime.Now;
                    obj.Timestamp = DateTime.Now;
                    _unitOfWork.Customer.Add(obj);
                    _unitOfWork.Save();
                }
                else
                {
                    obj.Timestamp = DateTime.Now;
                    _unitOfWork.Customer.Update(obj);
                    _unitOfWork.Save();
                }

                return RedirectToAction("Index");
            }

            return PartialView("_Upsert", obj);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var customer = _unitOfWork.Customer.Get(c => c.ID == id);

            if (customer == null)
            {
                return NotFound();
            }

            _unitOfWork.Customer.Remove(customer);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
