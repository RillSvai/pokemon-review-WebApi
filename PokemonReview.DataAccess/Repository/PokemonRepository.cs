using Microsoft.EntityFrameworkCore;
using PokemonReview.Models.Models;
using PokemonReview.DataAccess.Repository.IRepository;
using PokemonReview.DataAccess.Data;
using PokemonReview.Models.CreateDto;

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
			return await _db.Pokemons.AsNoTracking().FirstOrDefaultAsync(pokemon => pokemon.Id == pokemonId) is not null;
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

        public async Task InsertPokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
			Owner? owner = await _db.Owners.FirstOrDefaultAsync(o => o.Id == ownerId);
			Category? category = await  _db.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
			await _db.PokemonOwners.AddAsync(new PokemonOwner { Owner = owner, Pokemon = pokemon});
			await _db.PokemonCategories.AddAsync(new PokemonCategory { Category = category, Pokemon = pokemon});
			await _db.Pokemons.AddAsync(pokemon);
        }

        public void Update(Pokemon pokemon)
		{
			_db.Pokemons.Update(pokemon);
		}
    }
}
