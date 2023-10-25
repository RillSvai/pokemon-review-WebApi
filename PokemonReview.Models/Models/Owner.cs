using System.ComponentModel.DataAnnotations;

namespace PokemonReview.Models.Models
{
	public class Owner
	{
		[Key]
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Gym { get; set; }
		public Country Country { get; set; }
		public IEnumerable<PokemonOwner> PokemonOwners { get; set; }
	}
}
