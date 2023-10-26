using PokemonReview.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReview.DataAccess.Repository.IRepository
{
	public interface IReviewRepository : IRepository<Review>
	{
		public void Update(Review review);
		public Task<Reviewer?> GetReviewer(int reviewId);
		public Task<Pokemon?> GetReviewedPokemon(int reviewId);
	}
}
