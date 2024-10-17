using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQME.Models
{
    public class PurchaseOrderItems
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Purchase Order")]
        public int PurchaseOrderID { get; set; }
        public PurchaseOrder? PurchaseOrder { get; set; }

        [Required]
        [Display(Name = "SKU")]
        public int SKUID { get; set; }
        public SKU? SKU { get; set; }

        [Required]
        public int Quantity { get; set; }

        [NotMapped]
        public decimal Price => SKU != null ? SKU.UnitPrice * Quantity : 0;
        public DateTime Timestamp { get; set; }
    }
}
