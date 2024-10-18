using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQME.Models
{
    public class PurchaseOrder
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerID { get; set; }
        public Customer? Customer { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(PurchaseOrder), "ValidateDateOfDelivery")]
        [Display(Name = "Date of Delivery")]
        public DateTime DateOfDelivery { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        [Display(Name = "Amount Due")]
        public decimal AmountDue { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime Timestamp { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Order item is required.")]
        public ICollection<PurchaseOrderItems>? PurchaseOrderItems { get; set; }

        public static ValidationResult? ValidateDateOfDelivery(DateTime dateOfDelivery, ValidationContext context)
        {
            if (dateOfDelivery < DateTime.Now.AddDays(1))
            {
                return new ValidationResult("Date of delivery cannot be less than tomorrow's date.");
            }
            return ValidationResult.Success;
        }
    }

    public enum OrderStatus
    {
        New = 1,
        Completed = 2,
        Cancelled = 3
    }
}
