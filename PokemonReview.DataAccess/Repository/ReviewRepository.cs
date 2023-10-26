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
	public class ReviewRepository : Repository<Review>, IReviewRepository
	{
		private readonly ApplicationDbContext _db;
        public ReviewRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;	
        }
		public async Task<Pokemon?> GetReviewedPokemon(int reviewId)
		{
			return await _db.Pokemons.FirstOrDefaultAsync(pokemon => pokemon.Reviews.Any(review => review.Id == reviewId));
		}

		public async Task<Reviewer?> GetReviewer(int reviewId)
		{
			return await _db.Reviewers.FirstOrDefaultAsync(reviewer => reviewer.Reviews.Any(review => review.Id == reviewId));
		}

		public void Update(Review review)
		{
			_db.Reviews.Update(review);
		}
	}
}
