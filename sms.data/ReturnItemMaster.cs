using System;
using System.Collections.Generic;

namespace sms.Data;

public partial class ReturnItemMaster
{
    public int Id { get; set; }

    public int ReturnMasterId { get; set; }

    public string ItemName { get; set; } = null!;

    public string ItemNumber { get; set; } = null!;

    public double Mrp { get; set; }

    public double Total { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }
}
