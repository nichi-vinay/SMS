using System;
using System.Collections.Generic;

namespace sms.Data;

public partial class VendorMaster
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public long Mobile { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public int TaxTypeMasterId { get; set; }

    public long TaxNumber { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }
}
