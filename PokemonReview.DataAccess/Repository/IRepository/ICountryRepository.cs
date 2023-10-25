using PokemonReview.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReview.DataAccess.Repository.IRepository
{
	public interface ICountryRepository : IRepository<Country>
	{
		public void Update(Country country);
		public Task<bool> Exists(int countryId);
		public Task<IEnumerable<Owner>> GetOwners(int countryId);

	}
}
