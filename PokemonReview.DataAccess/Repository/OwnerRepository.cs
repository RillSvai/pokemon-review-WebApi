using Microsoft.EntityFrameworkCore;
using PokemonReview.DataAccess.Data;
using PokemonReview.DataAccess.Repository.IRepository;
using PokemonReview.Models.Models;

namespace PokemonReview.DataAccess.Repository
{
	public class OwnerRepository : Repository<Owner>, IOwnerRepository
	{
		private readonly ApplicationDbContext _db;

		public OwnerRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
        async public Task<bool> Exists(int ownerId)
		{
			return await _db.Owners.FirstOrDefaultAsync(owner => owner.Id == ownerId) is not null;
		}

		async public Task<Country?> GetCountry(int ownerId)
		{
			return await _db.Countries.FirstOrDefaultAsync(country => country.Owners.Any(owner => owner.Id == ownerId));
		}

		async public Task<IEnumerable<Pokemon>> GetPokemons(int ownerId)
		{
			return await _db.PokemonOwners.Where(pc => pc.OwnerId == ownerId).Select(pc => pc.Pokemon).ToListAsync();
		}

		public void Update(Owner owner)
		{
			_db.Owners.Update(owner);
		}
	}
}
