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
	public class ReviewerController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
        public ReviewerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
			_mapper = mapper;
        }
        [HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetAll() 
		{
			var reviewers = _mapper.Map<IEnumerable<ReviewerDto>>(await _unitOfWork.ReviewerRepo.GetAllAsync());
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState);
			}
			return Ok(reviewers);
		}
		[HttpGet("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Get(int id) 
		{
			var reviewer = _mapper.Map<ReviewerDto>(await _unitOfWork.ReviewerRepo.GetAsync(reviewer => reviewer.Id == id));
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
		[HttpGet("{reviewerId:int}/reviews")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> GetReviews(int reviewerId)
		{
			var reviews = _mapper.Map<IEnumerable<ReviewDto>>(await _unitOfWork.ReviewerRepo.GetReviews(reviewerId));
			if (reviews is null || !reviews.Any())
			{
				return NoContent();
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(reviews);
		}
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Create([FromBody] ReviewerDto reviewer) 
		{
			if (reviewer is null)
			{
				return BadRequest(ModelState);
			}
			Reviewer? duplicate = await _unitOfWork.ReviewerRepo.GetAsync(r => r.LastName == reviewer.LastName);
			if (duplicate is not null) 
			{
				ModelState.AddModelError("","Entity already exists!");
			}
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			Reviewer mappedReviewer = _mapper.Map<Reviewer>(reviewer);
			await _unitOfWork.ReviewerRepo.InsertAsync(mappedReviewer);
			await _unitOfWork.Save();
			return NoContent();
		}
	}
}
