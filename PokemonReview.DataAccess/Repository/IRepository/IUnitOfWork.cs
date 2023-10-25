namespace PokemonReview.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		IPokemonRepository PokemonRepo { get; }

		public Task Save();
	}
}
