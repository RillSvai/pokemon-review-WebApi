using AutoMapper;
using PokemonReview.Models.Dto;
using PokemonReview.Models.Models;

namespace PokemonReview.Utility
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<Pokemon,PokemonDto>();
            CreateMap<Category,CategoryDto>();
            CreateMap<Country,CountryDto>();
            CreateMap<Owner,OwnerDto>();
        }
    }
}
