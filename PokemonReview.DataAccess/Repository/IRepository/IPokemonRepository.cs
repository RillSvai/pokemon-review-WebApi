using PokemonReview.Models.CreateDto;
using PokemonReview.Models.Models;

namespace PokemonReview.DataAccess.Repository.IRepository
{
	public interface IPokemonRepository : IRepository<Pokemon>
	{
		public void Update(Pokemon pokemon);
		public Task<bool> Exists(int pokemonId);
		public double GetRating(int pokemonId);
		public Task InsertPokemon(int ownerId,int categoryId,Pokemon pokemon);
	}
}
