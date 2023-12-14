using sms.viewmodels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderViewModel
{

    public int Id { get; set; }

    
    public int VendorId { get; set; }

   
    public int OrderTypeId { get; set; }

    public string OrderTypeName { get; set; }



    public string Name { get; set; } = null!;

  
    public string OrderName { get; set; } = null!;


    public string OrderNumber { get; set; } = null!;

    public DateTime OrderDate { get; set; }



    public string ShipmentDetails { get; set; } = null!;

    public bool IsCancelled { get; set; }

    public bool IsActive { get; set; }

    public bool IsSubmitted { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public List<OrderItemViewModel> OrderItems { get; set; }

}
