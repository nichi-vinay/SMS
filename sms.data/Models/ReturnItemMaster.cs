using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.data.Models
{
    public class ReturnItemMaster
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("ReturnMaster")]
        public int ReturnMasterId { get; set; }

        [Required]
        [ForeignKey("ItemMaster")]
        public int ItemID { get; set; }
        [Required]
        public double Mrp { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public ReturnMaster ReturnMaster { get; set; } = null!;

        public ItemMaster ItemMaster { get; set; } = null!;
    }
}
