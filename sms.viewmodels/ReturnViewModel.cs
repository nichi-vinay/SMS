﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.viewmodels
{
    public class ReturnViewModel
    {
        public int Id { get; set; }
       
        public int VendorId { get; set; }

        public string InvoiceName { get; set; } = null!;

        public string ReturnNumber { get; set; } = null!;

        public DateTime ReturnDate { get; set; }

        public string ShipmentDetails { get; set; } = null!;

        public string TaxNumber { get; set; } = null!;

        public bool IsCanceled { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
