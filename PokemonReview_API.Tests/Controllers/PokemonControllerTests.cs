using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PokemonReview.DataAccess.Repository;
using PokemonReview.DataAccess.Repository.IRepository;
using PokemonReview.Models.Dto;
using PokemonReview.Models.Models;
using PokemonReview_API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReview_API.Tests.Controllers
{
    public class PokemonControllerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PokemonController _pokemonController;
        public PokemonControllerTests() 
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
            _mapper = A.Fake<IMapper>();
            _pokemonController = new PokemonController(_unitOfWork, _mapper);
        }
        [Fact]
        public async void PokemonController_GetAll_ReturnsOK() 
        {
            //Arrange
            IEnumerable<PokemonDto> pokemonsDto = A.Fake<List<PokemonDto>>();
            IEnumerable<Pokemon> pokemons = A.Fake<IEnumerable<Pokemon>>();
            A.CallTo(() => _mapper.Map<IEnumerable<PokemonDto>>(pokemons)).Returns(pokemonsDto);
            
            //Act
            var result = await _pokemonController.GetAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
