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
        [ForeignKey("SalesMaster")]
        public int SalesmasterId { get; set; }

        public string ItemName { get; set; } = null!;

        public string ItemNumber { get; set; } = null!;

        public double Mrp { get; set; }

        public double? DiscountPercentage { get; set; }

        public double TotalPrice { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public SalesMaster SalesMaster { get; set; } = null!;
    }
}
