namespace PokemonReview_API.Models
{
	public class Owner
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Gym { get; set; }
		public Country Country { get; set; }
		public IEnumerable<PokemonOwner> PokemonOwners { get; set; }
	}
}
