using PokemonReview_API.Models;

namespace PokemonReview_API.Repository.IRepository
{
	public interface IPokemonRepository : IRepository<Pokemon>
	{
		public void Update(Pokemon pokemon);
	}
}
