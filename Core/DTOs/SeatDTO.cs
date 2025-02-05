using DataAccess.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs
{
	public class SeatDTO
	{
		public int Id { get; set; }
		public int Row { get; set; }
		public int Number { get; set; }

		public int HallId { get; set; }
		public Hall? Hall { get; set; }
	}
}
