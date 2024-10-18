using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQME.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "First name")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Last name")]
        public string? LastName { get; set; }

        [NotMapped]
        public string FullName => $"{LastName}, {FirstName}";

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be 10 digits")]
        [Display(Name = "Mobile Number")]
        public string? MobileNumber { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime Timestamp { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
