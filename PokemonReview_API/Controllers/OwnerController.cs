using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReview.DataAccess.Repository.IRepository;
using PokemonReview.Models.CreateDto;
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
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Create([FromBody] OwnerCreateDto owner)
		{
			if (owner is null) 
			{
				return BadRequest(ModelState);
			}
			Owner? duplicate = await _unitOfWork.OwnerRepo.GetAsync(o => o.LastName.Trim().ToLower() == owner.LastName.Trim().ToLower()); 
			if (duplicate is not null) 
			{
				ModelState.AddModelError("", "Entity already exists!");
				return BadRequest(ModelState);
			}
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState);
			}
			Owner mappedOwner = _mapper.Map<Owner>(owner);
			await _unitOfWork.OwnerRepo.InsertAsync(mappedOwner);
			await _unitOfWork.Save();
			return NoContent();
		}
		[HttpPut("{id:int}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Update(int id, [FromQuery] int countryId, [FromBody] OwnerDto owner) 
		{
			if (owner is null || owner.Id != id) 
			{
				return BadRequest(ModelState);
			}
			if (!await _unitOfWork.OwnerRepo.Exists(id)) 
			{
				return NotFound();
			}
			if (!ModelState.IsValid) 
			{
				return BadRequest();
			}
			Owner mappedOwner = _mapper.Map<Owner>(owner);
			mappedOwner.CountryId = countryId;	
			_unitOfWork.OwnerRepo.Update(mappedOwner);
			await _unitOfWork.Save();
			return NoContent();
		}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _unitOfWork.OwnerRepo.Exists(id))
            {
                return NotFound();
            }
            Owner owner = (await _unitOfWork.OwnerRepo.GetAsync(o => o.Id == id))!;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _unitOfWork.OwnerRepo.Remove(owner);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
