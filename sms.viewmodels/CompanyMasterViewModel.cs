using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.viewmodels
{
    public class CompanyMasterViewModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public byte[] Logo { get; set; } = null!;


       
        public int ItemID { get; set; }

        public double Mrp { get; set; }

        public double TotalPrice { get; set; }

        public bool IsActive { get; set; }
    }
}
