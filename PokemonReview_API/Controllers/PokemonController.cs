using Microsoft.AspNetCore.Mvc;
using PokemonReview.Models.Models;
using PokemonReview.DataAccess.Repository.IRepository;
using AutoMapper;
using PokemonReview.Models.Dto;

namespace PokemonReview_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PokemonController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

        public PokemonController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
			_mapper = mapper;
        }

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Pokemon>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		async public Task<IActionResult> GetAll() 
		{
			var pokemons = _mapper.Map<List<PokemonDto>>(await _unitOfWork.PokemonRepo.GetAllAsync()); 
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState);
			}
			return Ok(pokemons);
		}
		[HttpGet("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pokemon))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		async public Task<IActionResult> Get(int id) 
		{
			if (!await _unitOfWork.PokemonRepo.Exists(id)) 
			{
				return NotFound();
			}
			var pokemon = _mapper.Map<PokemonDto>(await _unitOfWork.PokemonRepo.GetAsync(filter: pokemon => pokemon.Id == id));
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState); 
			}
			return Ok(pokemon);
        }
		[HttpGet("{id:int}/rating")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(double))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetRating(int id) 
		{
			if (!await _unitOfWork.PokemonRepo.Exists(id)) 
			{
				return NotFound();
			}
			double rating = _unitOfWork.PokemonRepo.GetRating(id);
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState);
			}	
			return Ok(rating);
		}
	}
}
