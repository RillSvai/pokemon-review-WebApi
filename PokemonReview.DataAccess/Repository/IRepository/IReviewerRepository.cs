using PokemonReview.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReview.DataAccess.Repository.IRepository
{
	public interface IReviewerRepository : IRepository<Reviewer>
	{
		public void Update(Reviewer reviewer);
		public Task<IEnumerable<Review>> GetReviews(int reviewerId);
	}
}
