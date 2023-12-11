using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.data.Models
{
    public class SalesItemMaster
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("SalesMaster")]
        public int SalesmasterId { get; set; }
        [Required]
        [ForeignKey("ItemMaster")]
        public int ItemID { get; set; }
        [Required]
        [ForeignKey("TaxTypeMaster")]
        public int TaxTypeID {  get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Mrp { get; set; }
        [Required]
        public string Barcode { get; set; }
        public double? TaxPercentage { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public SalesMaster SalesMaster { get; set; } = null!;

        public ItemMaster ItemMaster { get; set; } = null!;

        public TaxTypeMaster TaxTypeMaster { get; set; } = null!;
    }
}
