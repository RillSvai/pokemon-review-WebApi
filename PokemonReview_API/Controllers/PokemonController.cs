using Microsoft.AspNetCore.Mvc;
using PokemonReview.Models.Models;
using PokemonReview.DataAccess.Repository.IRepository;
using AutoMapper;
using PokemonReview.Models.Dto;

namespace PokemonReview_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PokemonController : ControllerBase
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
			var pokemons = _mapper.Map<IEnumerable<PokemonDto>>(await _unitOfWork.PokemonRepo.GetAllAsync()); 
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
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Create([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] PokemonDto pokemon)
		{
			if (pokemon is null || !await _unitOfWork.OwnerRepo.Exists(ownerId) || !await _unitOfWork.CategoryRepo.Exists(categoryId)) 
			{
				return BadRequest(ModelState);
			}
			Pokemon? duplicate = await _unitOfWork.PokemonRepo.GetAsync(p => p.Name.Trim().ToLower() == pokemon.Name.Trim().ToLower());
			if (duplicate is not null)
			{
				ModelState.AddModelError("", "Entity already exists!");
				return BadRequest(ModelState);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			Pokemon mappedPokemon = _mapper.Map<Pokemon>(pokemon);
			await _unitOfWork.PokemonRepo.InsertPokemon(ownerId, categoryId, mappedPokemon);
			await _unitOfWork.Save();
			return NoContent();
		}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _unitOfWork.PokemonRepo.Exists(id))
            {
                return NotFound();
            }
			_unitOfWork.ReviewRepo.RemoveRange(await _unitOfWork.ReviewRepo.GetAllAsync(r => r.PokemonId == id));
            Pokemon pokemon = (await _unitOfWork.PokemonRepo.GetAsync(p => p.Id == id))!;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _unitOfWork.PokemonRepo.Remove(pokemon);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
