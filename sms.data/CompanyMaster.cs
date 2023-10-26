using System;
using System.Collections.Generic;

namespace sms.Data;

public partial class CompanyMaster
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public byte[] Logo { get; set; } = null!;

    public string ItemNumber { get; set; } = null!;

    public double Mrp { get; set; }

    public double TotalPrice { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }
}
