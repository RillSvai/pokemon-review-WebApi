using PokemonReview.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReview.DataAccess.Repository.IRepository
{
	public interface IOwnerRepository : IRepository<Owner>
	{
		public void Update(Owner owner);
		public Task<bool> Exists(int ownerId);
		public Task<Country?> GetCountry(int ownerId);
		public Task<IEnumerable<Pokemon>> GetPokemons(int ownerId);
	}
}
