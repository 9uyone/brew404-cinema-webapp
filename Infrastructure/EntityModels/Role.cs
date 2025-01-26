using DataAccess.Interfaces;


namespace DataAccess.EntityModels
{
	public class Role : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;

		public ICollection<Credit> Credits { get; set; } = new List<Credit>();
	}
}
