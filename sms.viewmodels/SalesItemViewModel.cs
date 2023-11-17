using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.viewmodels
{
    public class SalesItemViewModel
    {
        [Key]
        public int Id { get; set; }
        public int SalesmasterId { get; set; }
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public double Mrp { get; set; }
        public double? DiscountPercentage { get; set; }
        public double TotalPrice { get; set; }
       
        public string Barcode { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
