using System.ComponentModel.DataAnnotations;

namespace MovieApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Language { get; set; }
        [Required]
        public double Rating { get; set; }
        [Required]
        public double Price { get; set; }
        public string? Image { get; set; }
        public Genre Genre { get; set; }
        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

    }
}
