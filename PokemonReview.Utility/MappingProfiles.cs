using AutoMapper;
using PokemonReview.Models.CreateDto;
using PokemonReview.Models.Dto;
using PokemonReview.Models.Models;

namespace PokemonReview.Utility
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<Pokemon,PokemonDto>().ReverseMap();
            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<Country,CountryDto>().ReverseMap();
            CreateMap<Owner,OwnerDto>().ReverseMap();
            CreateMap<Review,ReviewDto>().ReverseMap();
            CreateMap<Reviewer,ReviewerDto>().ReverseMap();

            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CountryCreateDto, Country>();
            CreateMap<OwnerCreateDto, Owner>();
      
        }
    }
}
