using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReview.DataAccess.Repository.IRepository;
using PokemonReview.Models.Dto;
using PokemonReview.Models.Models;
using System.Collections;

namespace PokemonReview_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OwnerController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

        public OwnerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
			_mapper = mapper;
        }

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK,Type = typeof(IEnumerable<Owner>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		async public Task<IActionResult> GetAll() 
		{
			var owners = _mapper.Map<IEnumerable<OwnerDto>>(await _unitOfWork.OwnerRepo.GetAllAsync());
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState);
			}
			return Ok(owners);
		}
		[HttpGet("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Owner))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		async public Task<IActionResult> Get(int id) 
		{
			if (!await _unitOfWork.OwnerRepo.Exists(id)) 
			{
				return NotFound();
			}
			var owner = _mapper.Map<OwnerDto>(await _unitOfWork.OwnerRepo.GetAsync(owner => owner.Id == id));
			if (!ModelState.IsValid) 
			{
				return BadRequest();
			}
			return Ok(owner);
		}
		[HttpGet("{ownerId:int}/country")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Country))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		async public Task<IActionResult> GetCountry(int ownerId) 
		{
			var country = _mapper.Map<CountryDto>(await _unitOfWork.OwnerRepo.GetCountry(ownerId));
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState);
			}
			return Ok(country);
		}
		[HttpGet("{ownerId:int}/pokemons")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Pokemon>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		async public Task<IActionResult> GetPokemons(int ownerId) 
		{
			var pokemons = _mapper.Map<IEnumerable<PokemonDto>>(await _unitOfWork.OwnerRepo.GetPokemons(ownerId));
			if (!ModelState.IsValid) 
			{
				return BadRequest();
			}
			return Ok(pokemons);
		}
	}
}
