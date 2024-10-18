using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQME.Models
{
    public class SKU
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public string? Name { get; set; }

        [Required]
        [StringLength(50)]
        public string? Code { get; set; }

        [Required]
        [Display(Name = "Unit Price")]
        [Index(IsUnique = true)]
        public decimal UnitPrice { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime Timestamp { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
