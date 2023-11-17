using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.data.Models
{
    public class SalesMaster
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("CustomerMaster")]
        public int CustomerId { get; set; }
        [Required]
        [ForeignKey("CustomerTypeMaster")]
        public int customerTypeMasterId {  get; set; }
        [Required]
        public string InvoiceNumber { get; set; } = null!;
        [Required]
        public DateTime InvoiceDate { get; set; }

        public string? ShipmentDetails { get; set; }
        
        public byte[]? InvoiceCopy { get; set; }
        [Required]
        public DateTime ExpectedDelivery {get; set;}
        public string? TaxNumber { get; set;}
        public float? Cheque {  get; set;}
        public float? Cash {  get; set;}
        public float? Online { get; set;}
        public float? Cards {  get; set;}
        [Required]
        public bool IsCanceled { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public CustomerMaster CustomerMaster { get; set; } = null!;

        public CustomerTypeMaster CustomerTypeMaster { get; set; } = null!;
    }
}
