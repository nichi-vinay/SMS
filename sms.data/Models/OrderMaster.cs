using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.data.Models
{
    public class OrderMaster
    {

        [Key]
        public int Id { get; set; }
        [ForeignKey("VendorMaster")]
        public int VendorId { get; set; }
        [ForeignKey("OrderTypeMaster")]
        public int OrderTypeId { get; set; }

        public string OrderName { get; set; } = null!;

        public string OrderNumber { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        public string ShipmentDetails { get; set; } = null!;

        public bool IsCancelled { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public VendorMaster VendorMaster { get; set; } = null!;

        public OrderTypeMaster OrderTypeMaster { get; set; } = null!;
    }
}
