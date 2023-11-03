using System.ComponentModel.DataAnnotations;

namespace PokemonReview.Models.Models
{
	public class Review
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public double Rating { get; set; }
		public int ReviewerId { get; set; }
		public Reviewer Reviewer { get; set; }
		public int PokemonId { get; set; }
		public Pokemon? Pokemon { get; set; }
	}
}
