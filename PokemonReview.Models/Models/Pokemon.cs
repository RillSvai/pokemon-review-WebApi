using System.ComponentModel.DataAnnotations;

namespace PokemonReview.Models.Models
{
	public class Pokemon
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }
		public IEnumerable<Review> Reviews { get; set; }
		public IEnumerable<PokemonCategory> PokemonCategories { get; set; }	
		public IEnumerable<PokemonOwner> PokemonOwners { get; set; }
	}
}
