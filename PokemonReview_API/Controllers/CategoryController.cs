using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonReview.DataAccess.Repository.IRepository;
using PokemonReview.Models.CreateDto;
using PokemonReview.Models.Dto;
using PokemonReview.Models.Models;

namespace PokemonReview_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CategoryController : ControllerBase
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
		[HttpGet("{categoryId:int}/pokemons")]
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
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
        async public Task<IActionResult> Create([FromBody] CategoryCreateDto category)
		{
			if (category is null)
			{
				return BadRequest(ModelState);
			}
			Category? duplicate = await _unitOfWork.CategoryRepo.GetAsync(c => c.Name.Trim().ToLower() == category.Name.Trim().ToLower());
			if (duplicate is not null) 
			{
				ModelState.AddModelError("", $"Entity already exists!");
				return BadRequest(ModelState);
			}
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState);
			}
			Category mappedCategory = _mapper.Map<Category>(category);
			await _unitOfWork.CategoryRepo.InsertAsync(mappedCategory);
			await _unitOfWork.Save();
			return NoContent();
		}
		[HttpPut("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Update(int id, [FromBody] CategoryDto category)
		{
			if (category is null || category.Id != id)
			{
				return BadRequest(ModelState);
			}
			if (!await _unitOfWork.CategoryRepo.Exists(id))
			{
				return NotFound();
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			Category mappedCategory = _mapper.Map<Category>(category);
			_unitOfWork.CategoryRepo.Update(mappedCategory);
			await _unitOfWork.Save();
			return NoContent();
		}

	}
}
