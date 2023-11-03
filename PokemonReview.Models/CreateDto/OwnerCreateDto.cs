using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReview.Models.CreateDto
{
    public class OwnerCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gym { get; set; }
        public int CountryId { get; set; }
    }
}
