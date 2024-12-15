using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Category
    {
         
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}

