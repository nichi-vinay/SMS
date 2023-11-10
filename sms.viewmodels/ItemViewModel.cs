using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.viewmodels
{
    public class ItemViewModel
    {
        [Key]
        public int Id { get; set; }
        public string ItemName { get; set; } = null!;
        public string ItemNumber { get; set; } = null!;
        public int UnitTypeMasterId { get; set; }
        public bool IsActive { get; set; }
    }
}
