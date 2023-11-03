using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReview.DataAccess.Repository.IRepository;
using PokemonReview.Models.Dto;
using PokemonReview.Models.Models;

namespace PokemonReview_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewController : ControllerBase
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReviewController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetAll()
		{
			var reviews = _mapper.Map<IEnumerable<ReviewDto>>(await _unitOfWork.ReviewRepo.GetAllAsync());
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState);
			}
			return Ok(reviews);
		}
		[HttpGet("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Get(int id) 
		{
			var review = _mapper.Map<ReviewDto>(await _unitOfWork.ReviewRepo.GetAsync(review => review.Id == id));
			if (review is null)
			{
				return NoContent();
			}
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState);
			}
			return Ok(review);
		}
		[HttpGet("{reviewId:int}/reviewer")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetReviewer(int reviewId) 
		{
			var reviewer = _mapper.Map<ReviewerDto>(await _unitOfWork.ReviewRepo.GetReviewer(reviewId));
			if (reviewer is null)
			{
				return NoContent();
			}
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState);
			}
			return Ok(reviewer);
		}
		[HttpGet("{reviewId:int}/pokemon")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetPokemon(int reviewId)
		{
			var pokemon = _mapper.Map<PokemonDto>(await _unitOfWork.ReviewRepo.GetReviewedPokemon(reviewId));
			if (pokemon is null)
			{
				return NoContent();
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(pokemon);
		}
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Create([FromQuery] int reviewerId, [FromQuery] int pokemonId, ReviewDto review)
		{
			if (review is null || !await _unitOfWork.PokemonRepo.Exists(pokemonId) || (await _unitOfWork.ReviewerRepo.GetAsync(r => r.Id == reviewerId)) is null)
			{
				return BadRequest();
			}
			Review? duplicate = await _unitOfWork.ReviewRepo.GetAsync(r => r.Title.Trim().ToLower() == review.Title.Trim().ToLower());
			if (duplicate is not null)
			{
				ModelState.AddModelError("", "Entity already exists!");
				return BadRequest(ModelState);
			}
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState);
			}

			Review mappedReview = _mapper.Map<Review>(review);
			mappedReview.PokemonId = pokemonId;
			mappedReview.ReviewerId = reviewerId;
			await _unitOfWork.ReviewRepo.InsertAsync(mappedReview);
			await _unitOfWork.Save();
			return NoContent();
		}
	}
}
