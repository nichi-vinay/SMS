using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.viewmodels
{
    public class OrderTypeViewModel
    {
        [Key]
        public int Id { get; set; }

        public string OrderTypeName { get; set; } = null!;

        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
