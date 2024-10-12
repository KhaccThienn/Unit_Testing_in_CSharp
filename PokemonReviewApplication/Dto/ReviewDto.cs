using PokemonReviewApplication.Models;

namespace PokemonReviewApplication.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}
