using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day8First.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int? Id { get; set; }
        public string Title { get; set; }
        public string? Image { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Language { get; set; }
        public ICollection<Comment> Comments { get; set; }
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public List<User> LikedBy { get; set; }
    }
}
