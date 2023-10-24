using Microsoft.AspNetCore.Mvc;
using PokemonReview_API.Models;
using PokemonReview_API.Repository.IRepository;

namespace PokemonReview_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PokemonController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
        public PokemonController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Pokemon>))]
		async public Task<IActionResult> GetAll() 
		{
			IEnumerable<Pokemon> pokemons = await _unitOfWork.PokemonRepo.GetAllAsync(); 
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState);
			}
			return Ok(pokemons);
		}

	}
}
