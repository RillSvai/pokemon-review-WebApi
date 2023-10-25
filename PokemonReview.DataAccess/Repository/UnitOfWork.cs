using PokemonReview.DataAccess.Data;
using PokemonReview.DataAccess.Repository.IRepository;

namespace PokemonReview.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
			PokemonRepo = new PokemonRepository(db);
        }
        public IPokemonRepository PokemonRepo { get; private set; }	

		async public Task Save()
		{
			await _db.SaveChangesAsync();
		}
	}
}
