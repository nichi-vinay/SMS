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
        public string Name { get; set; } = null!;

        public byte[] Logo { get; set; } = null!;


        [ForeignKey("ItemMaster")]
        public int ItemID { get; set; }

        public double Mrp { get; set; }

        public double TotalPrice { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public ItemMaster ItemMaster { get; set; }=null!;
    }
}
