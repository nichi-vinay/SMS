using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.data.Models
{
    public class VendorMaster
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public long Mobile { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        [ForeignKey("TaxTypeMaster")]
        public int TaxTypeMasterId { get; set; }

        public long TaxNumber { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public TaxTypeMaster TaxTypeMaster { get; set; } = null!; // Navigation property
    }
}
