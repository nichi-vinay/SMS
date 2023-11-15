using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.data.Models
{
    public class ItemMaster
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string ItemName { get; set; } = null!;
        [Required]
        public string ItemNumber { get; set; } = null!;
        [Required]
        [ForeignKey("UnitTypeMaster")]
        public int UnitTypeMasterId { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public UnitTypeMaster UnitTypeMaster { get; set; } = null!;
    }
}
