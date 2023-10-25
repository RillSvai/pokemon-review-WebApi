namespace PokemonReview.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		IPokemonRepository PokemonRepo { get; }
		ICategoryRepository CategoryRepo { get; }
		public Task Save();
	}
}
