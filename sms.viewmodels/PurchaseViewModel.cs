using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.viewmodels
{
    public class PurchaseViewModel
    {
        [Key]
        public int Id { get; set; }    
        public int VendorId { get; set; }
        public string SupplierName {  get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public DateTime InvoiceDate { get; set; }
        public string? ShipmentDetails { get; set; }
        
        public double DiscountPercentage {  get; set; }
        public bool IsSubmitted { get; set; }
        public bool IsActive { get; set; }
        public float? Cheque { get; set; }
        public float? Cash { get; set; }
        public float? Online { get; set; }
        public float? Cards { get; set; }
        public float? TotalDiscount { get; set; }
        public float? TotalTax { get; set; }
        public float? TotalAmount { get; set; }
        public float? TotalPaid { get; set; }
        public List<PurchaseItemViewModel> PurchaseItems { get; set; }
    }
}
