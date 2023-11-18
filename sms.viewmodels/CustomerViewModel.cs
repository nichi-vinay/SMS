using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using sms.data.Models;

namespace sms.viewmodels
{
    public class CustomerViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
        

        public long Mobile { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? Comments { get; set; }

        public DateTime? FollowUpdate { get; set; }

        public bool IsWhatsapp { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int EnquirytypeId { get; set; }
    }
}
