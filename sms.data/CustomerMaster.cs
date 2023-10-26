using System;
using System.Collections.Generic;

namespace sms.Data;

public partial class CustomerMaster
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int EnquirytypeId { get; set; }

    public string? Email { get; set; }

    public long Mobile { get; set; }

    public string? Address { get; set; }

    public string? Comments { get; set; }

    public DateTime? FollowUpdate { get; set; }

    public bool IsWhatsapp { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }
}
