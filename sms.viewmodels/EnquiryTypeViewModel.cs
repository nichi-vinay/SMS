using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.viewmodels
{
    public class EnquiryTypeViewModel
    {
        [Key]
        public int Id { get; set; }

        public string EnquiryTypeName { get; set; } = null!;

        public bool IsActive { get; set; }

    }
}
