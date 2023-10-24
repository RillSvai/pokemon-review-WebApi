using PokemonReview_API.Data;
using PokemonReview_API.Models;
using PokemonReview_API.Repository.IRepository;

namespace PokemonReview_API.Repository
{
	public class PokemonRepository : Repository<Pokemon>, IPokemonRepository
	{
		private readonly ApplicationDbContext _db;

        public PokemonRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Pokemon pokemon)
		{
			_db.Pokemons.Update(pokemon);
		}
	}
}
