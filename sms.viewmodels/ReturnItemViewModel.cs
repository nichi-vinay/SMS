using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.viewmodels
{
    public class ReturnItemViewModel
    {
        [Key]
        public int Id { get; set; }
        public int ReturnMasterId { get; set; }
        public int ItemID { get; set; }
        public double Mrp { get; set; }
        public double Total { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
