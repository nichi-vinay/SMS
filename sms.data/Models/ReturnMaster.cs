using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.data.Models
{
    public class ReturnMaster
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("VendorMaster")]
        public int VendorId { get; set; }
        [Required]
        public string InvoiceName { get; set; } = null!;
        [Required]
        public string ReturnNumber { get; set; } = null!;
        [Required]
        public DateTime ReturnDate { get; set; }
        [Required]
        public string ShipmentDetails { get; set; } = null!;
        [Required]
        public string TaxNumber { get; set; } = null!;
        [Required]
        public bool IsCanceled { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public VendorMaster VendorMaster { get; set; } = null!;
    }
}
