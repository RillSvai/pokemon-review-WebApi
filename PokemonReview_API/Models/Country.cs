﻿namespace PokemonReview_API.Models
{
	public class Country
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public IEnumerable<Owner> Owners { get; set; }
	}
}
