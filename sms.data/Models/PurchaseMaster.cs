using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.data.Models
{
    public class PurchaseMaster
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("VendorMaster")]
        public int VendorId { get; set; }
        [Required]
        public string InvoiceNumber { get; set; } = null!;
        [Required]
        public DateTime InvoiceDate { get; set; }

        public string? ShipmentDetails { get; set; }
        [Required]
        public bool IsSubmitted { get; set; }
        [Required]
        public bool IsActive { get; set; }
      
        public float? Cheque { get; set; }
        public float? Cash { get; set; }
        public float? Online { get; set; }
        public float? Cards { get; set; }

        public float? TotalDiscount{ get;set;}

        public float? TotalTax { get; set; }
        public float? TotalAmount { get; set; }
        public float? TotalPaid { get; set; }
        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public VendorMaster VendorMaster { get; set; } = null!;

        public List<PurchaseItemMaster> purchaseItemMasters { get; set; }

        
    }
}
