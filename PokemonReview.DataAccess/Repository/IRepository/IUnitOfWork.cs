namespace PokemonReview_API.Repository.IRepository
{
	public interface IUnitOfWork
	{
		IPokemonRepository PokemonRepo { get; }

		public Task Save();
	}
}
