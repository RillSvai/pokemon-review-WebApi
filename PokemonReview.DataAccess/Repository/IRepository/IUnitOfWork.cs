namespace PokemonReview.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		IPokemonRepository PokemonRepo { get; }
		ICategoryRepository CategoryRepo { get; }
		ICountryRepository CountryRepo { get; }
		IOwnerRepository OwnerRepo { get; }
		IReviewRepository ReviewRepo { get; }
		IReviewerRepository ReviewerRepo { get; }
		public Task Save();
	}
}
