using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Customer
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(300)]
        public string Address { get; set; }
    }
}
