using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.viewmodels
{
    public class OrderItemViewModel
    {
        [Key]
        public int Id { get; set; }
        public int OrderMasterId { get; set; }
        public int ItemID { get; set; }
   
        public bool IsActive { get; set; }

        public string Quantity { get; set; }
        public bool IsSubmitted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
