using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Auth;
using DataAccess.EntityModels;
using DataAccess.Models;

namespace BusinessLogic.Helpers
{
    public class MapperProfile : Profile
    {
		public MapperProfile()
		{
			CreateMap<Movie, MovieDTO>()
				.ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate.ToString("yyyy-MM-dd")));
				//.ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => new GenreDTO { Id = g.Id, Name = g.Name })));

			CreateMap<MovieDTO, Movie>()
				.ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => ParseReleaseDate(src.ReleaseDate)));
		
			CreateMap<Genre, GenreDTO>().ReverseMap();
			CreateMap<Actor, ActorDTO>().ReverseMap();
			CreateMap<Hall, HallDTO>().ReverseMap();

			CreateMap<Session, SessionDTO>().ReverseMap();
			CreateMap<Seat, SeatDTO>().ReverseMap();

			/*CreateMap<TicketDTO, Ticket>()
				.ForMember(t => t.Seat, opt => opt.AddTransform(s => new Seat { Id = s.Id }))
				.ForMember(t => t.Session, opt => opt.AddTransform(s => new Session { Id = s.Id } ));*/

			CreateMap<Ticket, TicketDTO>().ReverseMap();

			CreateMap<RegisterDTO, User>()
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User"))
				.ForMember(dest => dest.BirthDate, opt => opt.Condition(src => src.BirthDate != default))
				.ForMember(dest => dest.PhoneNumber, opt => opt.Condition(src => !string.IsNullOrEmpty(src.PhoneNumber)));
		}
		
		private static DateTime ParseReleaseDate(string? releaseDate)
		{
			return DateTime.TryParse(releaseDate, out var date) ? date : DateTime.MinValue;
		}
	}
}