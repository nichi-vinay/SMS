using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.data.Models
{
    public class OrderTypeMaster
    {
        [Key]
        public int Id { get; set; }

        public string OrderTypeName { get; set; } = null!;

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int ModifiedBy { get; set; }
    }
}
