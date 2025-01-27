using DataAccess.Interfaces;


namespace DataAccess.EntityModels
{
	public class Role : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;

		public ICollection<CrewMember> Crew { get; set; } = new List<CrewMember>();
	}
}
