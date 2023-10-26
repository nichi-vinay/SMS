using System;
using System.Collections.Generic;

namespace sms.Data;

public partial class OrderTypeMaster
{
    public int Id { get; set; }

    public string OrderTypeName { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int ModifiedBy { get; set; }
}
