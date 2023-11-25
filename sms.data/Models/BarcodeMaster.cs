using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.data.Models
{
    public class BarcodeMaster
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Enter Barcode Text")]
        public string BarcodeText {  get; set; } 
    }
}
