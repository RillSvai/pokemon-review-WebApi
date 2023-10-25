using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReview.DataAccess.Repository.IRepository;
using PokemonReview.Models.Dto;
using PokemonReview.Models.Models;

namespace PokemonReview_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CategoryController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;	
			_mapper = mapper;
        }

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Pokemon>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		async public Task<IActionResult> GetAll() 
		{
			var categories = _mapper.Map<IEnumerable<CategoryDto>>(await _unitOfWork.CategoryRepo.GetAllAsync());
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState);	
			}
			return Ok(categories);
		}
		[HttpGet("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pokemon))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		async public Task<IActionResult> Get(int id) 
		{
			if (!await _unitOfWork.CategoryRepo.Exists(id)) 
			{
				return NotFound();
			}
			var category = _mapper.Map<CategoryDto>(await _unitOfWork.CategoryRepo.GetAsync(category => category.Id == id));
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState);
			}
			return Ok(category);
		}
		[HttpGet("pokemons/{categoryId:int}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Pokemon>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		async public Task<IActionResult> GetPokemons(int categoryId) 
		{
			var pokemons = _mapper.Map<IEnumerable<PokemonDto>>(await _unitOfWork.CategoryRepo.GetPokemons(categoryId));
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState);
			}	
			return Ok(pokemons);
		}
	}
}
