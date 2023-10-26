using System;
using System.Collections.Generic;

namespace sms.Data;

public partial class UserMaster
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public int RoleId { get; set; }

    public string? EmailId { get; set; }

    public long? MobileNo { get; set; }

    public bool IsActive { get; set; }

    public string Password { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }
}
