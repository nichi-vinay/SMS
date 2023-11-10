﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.data.Models
{
    public class CustomerMaster
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        
        [ForeignKey ("EnquiryTypeMaster")]
        public int EnquirytypeId { get; set; }

        public string? Email { get; set; }

        public long Mobile { get; set; }

        public string? Address { get; set; }

        public string? Comments { get; set; }

        public DateTime? FollowUpdate { get; set; }

        public bool IsWhatsapp { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public EnquiryTypeMaster EnquiryTypeMaster { get; set; } = null!;
        
    }
}
