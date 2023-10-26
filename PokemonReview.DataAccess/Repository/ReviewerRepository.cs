using Microsoft.EntityFrameworkCore;
using PokemonReview.DataAccess.Data;
using PokemonReview.DataAccess.Repository.IRepository;
using PokemonReview.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReview.DataAccess.Repository
{
	public class ReviewerRepository : Repository<Reviewer>, IReviewerRepository
	{
		private readonly ApplicationDbContext _db;

        public ReviewerRepository(ApplicationDbContext db) : base(db)
        {
			_db = db;
        }
        public async Task<IEnumerable<Review>> GetReviews(int reviewerId)
		{
			return await _db.Reviews.Where(review => review.Reviewer.Id == reviewerId).ToListAsync();
		}

		public void Update(Reviewer reviewer)
		{
			_db.Reviewers.Update(reviewer);
		}
	}
}
