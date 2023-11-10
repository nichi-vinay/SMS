using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.data.Models
{
    public class OrderItmeMaster
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("OrderMaster")]
        public int OrderMasterId { get; set; }

        public string ItemName { get; set; } = null!;

        public string ItemNumber { get; set; } = null!;

        public bool IsProcessed { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int ModifiedBy { get; set; }

        public OrderMaster OrderMaster { get; set; } = null!;
    }
}
