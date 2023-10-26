using System;
using System.Collections.Generic;

namespace sms.Data;

public partial class DashboardMaster
{
    public int Id { get; set; }

    public byte[] Image { get; set; } = null!;

    public bool IsAvilable { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }
}
