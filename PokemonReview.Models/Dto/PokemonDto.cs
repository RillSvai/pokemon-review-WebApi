using PokemonReview.Models.Models;

namespace PokemonReview_API.Dto
{
	public class PokemonDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }
	}
}
