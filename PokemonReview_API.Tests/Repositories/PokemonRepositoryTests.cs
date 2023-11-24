using PokemonReview.DataAccess.Data;
using PokemonReview.DataAccess.Repository;
using PokemonReview.DataAccess.Repository.IRepository;
using PokemonReview.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReview_API.Tests.Repositories
{
    public class PokemonRepositoryTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<Pokemon> pokemons;
        private readonly List<Review> reviews;
        public PokemonRepositoryTests()
        {
            pokemons = new List<Pokemon>()
            {
                new Pokemon() {Id = 1, Name = "Test1", BirthDate = DateTime.Now },
                new Pokemon() {Id = 2, Name = "Test2", BirthDate = DateTime.Now },
            };
            reviews = new List<Review>()
            {
                new Review() {Id = 1, PokemonId = 1, Rating = 2 , Text = "", Title = ""},
                new Review() {Id = 2, PokemonId = 1, Rating = 4, Text = "", Title = ""},
                new Review() {Id = 3, PokemonId = 2, Rating = 5, Text = "", Title = ""},
            };
            ApplicationDbContext db = Helper.GetDbContext();
            db.Pokemons.AddRange(pokemons);
            db.Reviews.AddRange(reviews);
            db.SaveChanges();
            _unitOfWork = new UnitOfWork(db);
        }
        [Fact]
        public async Task PokemonRepository_Get_ReturnsPokemon() 
        {
            //Arrange
            int id = 2;

            //Act
            var result =  await _unitOfWork.PokemonRepo.GetAsync(p => p.Id == id);

            //Assert
            result.Should().NotBeNull();
            result!.Name.Should().BeEquivalentTo("test2");
            result.Should().BeEquivalentTo(pokemons[1]);
        }
        [Fact]
        public void PokemonRepository_GetRating_ReturnDoubleBetweenZeroAndFive() 
        {
            //Arrange
            int id = 1;

            //Act
            var result = _unitOfWork.PokemonRepo.GetRating(id);

            //Assert
            result.Should().BeInRange(1,5);
        }
    }
}
