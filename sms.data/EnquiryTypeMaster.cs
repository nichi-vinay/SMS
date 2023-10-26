using System;
using System.Collections.Generic;

namespace sms.Data;

public partial class EnquiryTypeMaster
{
    public int Id { get; set; }

    public string EnquiryTypeName { get; set; } = null!;

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }
}
