using Microsoft.EntityFrameworkCore;
using PokemonReview.DataAccess.Data;
using PokemonReview.DataAccess.Repository.IRepository;
using PokemonReview.Models.Models;

namespace PokemonReview.DataAccess.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        async public Task<bool> Exists(int categoryId)
		{
			return await _db.Categories.AsNoTracking().FirstOrDefaultAsync(category => category.Id == categoryId) is not null;
		}

		async public Task<IEnumerable<Pokemon>?> GetPokemons(int categoryId)
		{
			return await _db.PokemonCategories.Where(pc => pc.CategoryId == categoryId).Select(pc => pc.Pokemon).ToListAsync();
		}

		public void Update(Category category)
		{
			_db.Categories.Update(category);
		}
	}
}
