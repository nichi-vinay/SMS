using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.viewmodels
{
    public class PurchaseItemViewModel
    {

        public int Id { get; set; }
        public int PurchasemasterId { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; } = null!;
        public string ItemNumber { get; set; } = null!;
        public int Quantity { get; set; }
        public double Mrp { get; set; }
        public double DiscountPercentage { get; set; }
        public double Totalprice { get; set; }
        public bool IsActive { get; set; }
        public string Barcode { get; set; }
    
    }
}
