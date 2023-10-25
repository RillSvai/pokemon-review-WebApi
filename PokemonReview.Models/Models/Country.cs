using System.ComponentModel.DataAnnotations;

namespace PokemonReview.Models.Models
{
	public class Country
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public IEnumerable<Owner> Owners { get; set; }
	}
}
