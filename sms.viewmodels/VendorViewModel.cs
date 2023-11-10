using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using sms.data.Models;

namespace sms.viewmodels
{
    public class VendorViewModel
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

        public TaxTypeMaster TaxTypeMaster { get; set; } = null!;
    }
}