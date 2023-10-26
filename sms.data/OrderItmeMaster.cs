using System;
using System.Collections.Generic;

namespace sms.Data;

public partial class OrderItmeMaster
{
    public int Id { get; set; }

    public int OrderMasterId { get; set; }

    public string ItemName { get; set; } = null!;

    public string ItemNumber { get; set; } = null!;

    public bool IsProcessed { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int ModifiedBy { get; set; }
}
