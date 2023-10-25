using System.ComponentModel.DataAnnotations;

namespace PokemonReview.Models.Models
{
	public class Reviewer
	{
		[Key]
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public IEnumerable<Review> Reviews { get; set;}
	}
}
