using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQME.Models.ViewModel
{
    public class PurchaseOrderVM
    {
        public PurchaseOrder PurchaseOrder { get; set; }
        public IEnumerable<SelectListItem>? CustomerList { get; set; }
        public IEnumerable<SelectListItem>? SKUList { get; set; }
        public IEnumerable<SelectListItem>? StatusList { get; set; }
    }
}
