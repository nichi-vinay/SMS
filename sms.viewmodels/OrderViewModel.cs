using sms.viewmodels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderViewModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("VendorMaster")]
    public int VendorId { get; set; }

    [Required]
    [ForeignKey("OrderTypeMaster")]
    public int OrderTypeId { get; set; }

    [Required]
    public string OrderName { get; set; } = null!;

    [Required]
    public string OrderNumber { get; set; } = null!;

    [Required]
    public DateTime OrderDate { get; set; }

    [Required]
    public string ShipmentDetails { get; set; } = null!;

    public bool IsCancelled { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

}
