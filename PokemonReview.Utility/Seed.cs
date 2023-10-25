
using PokemonReview.DataAccess.Data;
using PokemonReview.Models.Models;

namespace PokemonReview.Utility
{
	public class Seed
	{
		private readonly ApplicationDbContext _db;
		public Seed(ApplicationDbContext db)
		{
			_db = db;
		}
		public void SeedDataContext()
		{
			if (!_db.PokemonOwners.Any())
			{
				var pokemonOwners = new List<PokemonOwner>()
				{
					new PokemonOwner()
					{
						Pokemon = new Pokemon()
						{
							Name = "Pikachu",
							BirthDate = new DateTime(1903,1,1),
							PokemonCategories = new List<PokemonCategory>()
							{
								new PokemonCategory { Category = new Category() { Name = "Electric"}}
							},
							Reviews = new List<Review>()
							{
								new Review { Title="Pikachu",Text = "Pickahu is the best pokemon, because it is electric", Rating = 5,
								Reviewer = new Reviewer(){ FirstName = "Kiril", LastName = "Kvas" } },
								new Review { Title="Pikachu", Text = "Pickachu is the best a killing rocks", Rating = 5,
								Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
								new Review { Title="Pikachu",Text = "Pickchu, pickachu, pikachu", Rating = 1,
								Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
							}
						},
						Owner = new Owner()
						{
							FirstName = "Jack",
							LastName = "London",
							Gym = "Brocks Gym",
							Country = new Country()
							{
								Name = "Kanto"
							}
						}
					},
					new PokemonOwner()
					{
						Pokemon = new Pokemon()
						{
							Name = "Squirtle",
							BirthDate = new DateTime(1903,1,1),
							PokemonCategories = new List<PokemonCategory>()
							{
								new PokemonCategory { Category = new Category() { Name = "Water"}}
							},
							Reviews = new List<Review>()
							{
								new Review { Title= "Squirtle", Text = "squirtle is the best pokemon, because it is electric", Rating = 5,
								Reviewer = new Reviewer(){ FirstName = "Kiril", LastName = "Kvas" } },
								new Review { Title= "Squirtle",Text = "Squirtle is the best a killing rocks", Rating = 5,
								Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
								new Review { Title= "Squirtle", Text = "squirtle, squirtle, squirtle", Rating = 1,
								Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
							}
						},
						Owner = new Owner()
						{
							FirstName = "Harry",
							LastName = "Potter",
							Gym = "Mistys Gym",
							Country = new Country()
							{
								Name = "Saffron City"
							}
						}
					},
									new PokemonOwner()
					{
						Pokemon = new Pokemon()
						{
							Name = "Venasuar",
							BirthDate = new DateTime(1903,1,1),
							PokemonCategories = new List<PokemonCategory>()
							{
								new PokemonCategory { Category = new Category() { Name = "Leaf"}}
							},
							Reviews = new List<Review>()
							{
								new Review { Title="Veasaur",Text = "Venasuar is the best pokemon, because it is electric", Rating = 5,
								Reviewer = new Reviewer(){FirstName = "Kiril", LastName = "Kvas"} },
								new Review { Title="Veasaur",Text = "Venasuar is the best a killing rocks", Rating = 5,
								Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
								new Review { Title="Veasaur",Text = "Venasuar, Venasuar, Venasuar", Rating = 1,
								Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
							}
						},
						Owner = new Owner()
						{
							FirstName = "Ash",
							LastName = "Ketchum",
							Gym = "Ashs Gym",
							Country = new Country()
							{
								Name = "Millet Town"
							}
						}
					}
				};
				_db.PokemonOwners.AddRange(pokemonOwners);
				_db.SaveChanges();
			}
		}
	}
}
