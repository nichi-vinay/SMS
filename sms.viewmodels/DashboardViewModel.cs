﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.viewmodels
{
    public class DashboardViewModel
    {
        [Key]
        public int Id { get; set; }

        public byte[] Image { get; set; } = null!;

        public bool IsAvilable { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}