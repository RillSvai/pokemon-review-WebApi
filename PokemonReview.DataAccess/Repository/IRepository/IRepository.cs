using System.Linq.Expressions;

namespace PokemonReview.DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string includeProperties = "");
		public Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, string includeProperties = "");
		public Task InsertAsync(T entity);
		public void Remove(T entity);
		public void Remove(int id);
	}
}
