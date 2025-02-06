using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs
{
	public class MovieFilteredDTO
	{
		public List<int>? GenreIds { get; set; }
		public List<int>? ActorsIds { get; set; }
		public int? Year {get; set;}
		public string? SortBy { get; set; }
		public bool Descending { get; set; } = false;
	}
}
