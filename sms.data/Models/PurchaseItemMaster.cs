using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.data.Models
{
    public class PurchaseItemMaster
    {

       
        public int Id { get; set; }
        [Required]
        [ForeignKey("PurchaseMaster")]
        public int PurchasemasterId { get; set; }
        [Required]
        [ForeignKey("ItemMaster")]
        public int ItemID {  get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Mrp { get; set; }
        [Required]
        public double DiscountPercentage {  get; set; }
        [Required]
        public double Totalprice { get; set; }
   
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public string Barcode { get; set; }
        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public PurchaseMaster PurchaseMaster { get; set; } = null!;

        public ItemMaster ItemMaster { get; set; } = null!;
    }
}
