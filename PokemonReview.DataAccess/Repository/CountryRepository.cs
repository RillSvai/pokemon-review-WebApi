using Microsoft.EntityFrameworkCore;
using PokemonReview.DataAccess.Data;
using PokemonReview.DataAccess.Repository.IRepository;
using PokemonReview.Models.Models;

namespace PokemonReview.DataAccess.Repository
{
	public class CountryRepository : Repository<Country>, ICountryRepository
	{
		private readonly ApplicationDbContext _db;

        public CountryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        async public Task<bool> Exists(int countryId)
		{
			return await _db.Countries.FirstOrDefaultAsync(country => countryId == country.Id) is not null;
		}

		public async Task<IEnumerable<Owner>> GetOwners(int countryId)
		{
			return await _db.Owners.Where(owner => owner.Country.Id == countryId).ToListAsync();
		}

		public void Update(Country country)
		{
			_db.Countries.Update(country);
		}
	}
}
