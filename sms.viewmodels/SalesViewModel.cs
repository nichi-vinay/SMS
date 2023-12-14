using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace sms.viewmodels
{
    public class SalesViewModel
    {

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int customerTypeMasterId { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public string InvoiceDate { get; set; }
        public string? ShipmentDetails { get; set; }
        public int Quantity { get; set; }

        public float? TotalMRP { get; set; }
        public double? TotaDiscount { get; set; }
        public float? TotalPaid { get; set; }
        public byte[]? InvoiceCopy { get; set; }
        public string ExpectedDelivery { get; set; }
        public double? totaltax { get; set; }
        public double TotalAmount { get; set; }
        public string? TaxNumber { get; set; }
        public float? Cheque { get; set; }
        public float? Cash { get; set; }
        public float? Online { get; set; }
        public float? Cards { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsActive { get; set; }
        public List<SalesItemViewModel> SalesItems { get; set; }

        public List<SalesTransactionViewModel> SalesTransactions { get; set; }
    }
}
