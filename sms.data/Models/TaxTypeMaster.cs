using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.data.Models
{
    public class TaxTypeMaster
    {
        [Key]
        public int TAXTypeId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

    }
}
