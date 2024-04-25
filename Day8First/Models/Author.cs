using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day8First.Models
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        [Required(ErrorMessage ="Username field is required!")]
        [StringLength(20,MinimumLength =4,ErrorMessage ="Username must be at least 4 characters")]
        public string? Username { get; set; }
        [Required(ErrorMessage ="Email field is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password field is required")]
        [StringLength(30,MinimumLength =6,ErrorMessage ="Password must be at least 6 characters")]
        public string? Password { get; set; }
        public int BookCount { get; set; }
    }
}
