using PokemonReview.Models.Models;

namespace PokemonReview_API.Repository.IRepository
{
	public interface IPokemonRepository : IRepository<Pokemon>
	{
		public void Update(Pokemon pokemon);
		public Task<bool> Exists(int pokemonId);
		public double GetRating(int pokemonId);
	}
}
