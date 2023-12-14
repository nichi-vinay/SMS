using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.data.Models
{
    public  class CompanyInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CompanyName {  get; set; }

        public string CompanyAddress {  get; set; }

        public string Email {  get; set; }

        public long ContactNumber {  get; set; }

        public string TAXNumber {  get; set; }

        public int Postalcode {  get; set; }
        public int CreatedBy {  get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
