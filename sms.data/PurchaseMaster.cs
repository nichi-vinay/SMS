using System;
using System.Collections.Generic;

namespace sms.Data;

public partial class PurchaseMaster
{
    public int Id { get; set; }

    public int VendorId { get; set; }

    public string InvoiceNumber { get; set; } = null!;

    public DateTime InvoiceDate { get; set; }

    public string? ShipmentDetails { get; set; }

    public bool IsSubmitted { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }
}
