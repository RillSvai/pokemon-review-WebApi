using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReview.DataAccess.Repository.IRepository;
using PokemonReview.Models.CreateDto;
using PokemonReview.Models.Dto;
using PokemonReview.Models.Models;

namespace PokemonReview_API.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class CountryController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
        public CountryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
			_mapper = mapper;
        }

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK,Type = typeof(IEnumerable<Country>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		async public Task<IActionResult> GetAll() 
		{
			var countries = _mapper.Map<IEnumerable<CountryDto>>(await _unitOfWork.CountryRepo.GetAllAsync());
			if (!ModelState.IsValid) 
			{
				return BadRequest();
			}
			return Ok(countries);
		}
		[HttpGet("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Country))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		async public Task<IActionResult> Get(int id) 
		{
			if (!await _unitOfWork.CountryRepo.Exists(id)) 
			{
				return NotFound();
			}
			var country = _mapper.Map<CountryDto>(await _unitOfWork.CountryRepo.GetAsync(country => country.Id == id));
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState);
			}
			return Ok(country);
		}
		[HttpGet("{countryId:int}/owners")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Owner>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		async public Task<IActionResult> GetOwners(int countryId) 
		{
			var owners = _mapper.Map<IEnumerable<OwnerDto>>(await _unitOfWork.CountryRepo.GetOwners(countryId));
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState);
			}
			return Ok(owners);
		}
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        async public Task<IActionResult> Create([FromBody] CountryCreateDto country)
        {
            if (country is null)
            {
                return BadRequest(ModelState);
            }
            Country? duplicate = await _unitOfWork.CountryRepo.GetAsync(c => c.Name.Trim().ToLower() == country.Name.Trim().ToLower());
            if (duplicate is not null)
            {
                ModelState.AddModelError("", $"Entity already exists!");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Country mappedCountry = _mapper.Map<Country>(country);
            await _unitOfWork.CountryRepo.InsertAsync(mappedCountry);
            await _unitOfWork.Save();
            return NoContent();
        }
		[HttpPut("id:int")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] CountryDto country) 
		{
			if (country is null || country.Id != id) 
			{
				return BadRequest(ModelState);
			}
			if (!await _unitOfWork.CountryRepo.Exists(id))
			{
				return NotFound();
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			Country mappedCountry = _mapper.Map<Country>(country);
			_unitOfWork.CountryRepo.Update(mappedCountry);
			await _unitOfWork.Save();
			return NoContent();
		}
    }
}
