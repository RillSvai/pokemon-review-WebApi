using Microsoft.EntityFrameworkCore;
using PokemonReview.Models.Models;
using PokemonReview.DataAccess.Repository.IRepository;
using PokemonReview.DataAccess.Data;

namespace PokemonReview.DataAccess.Repository
{
	public class PokemonRepository : Repository<Pokemon>, IPokemonRepository
	{
		private readonly ApplicationDbContext _db;

        public PokemonRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

		async public Task<bool> Exists(int pokemonId)
		{
			return await _db.Pokemons.FirstOrDefaultAsync(pokemon => pokemon.Id == pokemonId) is not null;
		}

		public double GetRating(int pokemonId)
		{
			IEnumerable<Review> reviews = _db.Reviews.Where(review => review.Pokemon.Id == pokemonId);
			if (!reviews.Any()) 
			{
				return 0;
			}
			return reviews.Average(review => review.Rating);
		}
			
		public void Update(Pokemon pokemon)
		{
			_db.Pokemons.Update(pokemon);
		}
	}
}
