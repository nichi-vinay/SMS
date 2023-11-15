using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.data.Models
{
    public class CompanyMaster
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public byte[] Logo { get; set; } 


        [ForeignKey("ItemMaster")]
        [Required]
        public int ItemID { get; set; }
        [Required]
        public double Mrp { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public ItemMaster ItemMaster { get; set; }=null!;
    }
}
