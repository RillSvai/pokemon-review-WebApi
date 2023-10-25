using PokemonReview.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReview.DataAccess.Repository.IRepository
{
	public interface ICategoryRepository : IRepository<Category>
	{
		public void Update(Category category);
		public Task<IEnumerable<Pokemon>> GetPokemons(int categoryId);
		public Task<bool> Exists(int categoryId);
	}
}
