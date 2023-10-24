using Microsoft.EntityFrameworkCore;
using PokemonReview_API.Data;
using PokemonReview_API.Repository.IRepository;
using System.Linq.Expressions;

namespace PokemonReview_API.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		private readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
			_dbSet = _db.Set<T>();
        }
        async public virtual Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string includeProperties = "")
		{
			IQueryable<T> query = _dbSet;

			foreach (string str in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(str);
			}

			if (filter is not  null) 
			{
				query = query.Where(filter);
			}
			return await query.ToListAsync();
		}

		async public virtual Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, string includeProperties = "")
		{
			IQueryable<T> query = _dbSet;

			foreach (string str in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(str);
			}

			if (filter is not null)
			{
				query = query.Where(filter);
			}
			return await query.FirstOrDefaultAsync();
		}

		async public virtual Task InsertAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public virtual void Remove(T entity)
		{
			_dbSet.Remove(entity);
		}
		public virtual void Remove(int id)
		{
			T entity = _dbSet.Find(id);
			_dbSet.Remove(entity);
		}
	}
}
