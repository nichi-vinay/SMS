using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.data.Models
{
    public class PurchaseTransactionsMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("PurchaseMaster")]
        public int PurchasemasterId { get; set; }
        public float? Cheque { get; set; }
        public float? Cash { get; set; }
        public float? Online { get; set; }
        public float? Cards { get; set; }
        public float? TotalPaid { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public PurchaseMaster PurchaseMaster { get; set; }
    }
}
