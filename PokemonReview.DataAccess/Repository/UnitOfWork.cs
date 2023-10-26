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
			CategoryRepo = new CategoryRepository(db);
			CountryRepo = new CountryRepository(db);
			OwnerRepo = new OwnerRepository(db);
			ReviewRepo = new ReviewRepository(db);
			ReviewerRepo = new ReviewerRepository(db);
		}
		public IPokemonRepository PokemonRepo { get; private set; }

		public ICategoryRepository CategoryRepo {get; private set;}

		public ICountryRepository CountryRepo {get; private set;}
		public IOwnerRepository OwnerRepo { get; private set;}
		public IReviewRepository ReviewRepo { get; private set;}
		public IReviewerRepository ReviewerRepo { get; private set;}

		async public Task Save()
		{
			await _db.SaveChangesAsync();
		}
	}
}
