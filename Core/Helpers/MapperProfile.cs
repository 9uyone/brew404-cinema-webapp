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
				//.ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
				//.ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors));

			CreateMap<MovieDTO, Movie>()
				.ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => ParseReleaseDate(src.ReleaseDate)));
				//.ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
				//.ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors));
		
			CreateMap<Genre, GenreDTO>().ReverseMap();
			CreateMap<Actor, ActorDTO>().ReverseMap();
			CreateMap<Hall, HallDTO>().ReverseMap();

			CreateMap<Session, SessionDTO>().ReverseMap();
			CreateMap<Seat, SeatDTO>().ReverseMap();

			/*CreateMap<TicketDTO, Ticket>()
				.ForMember(t => t.Seat, opt => opt.AddTransform(s => new Seat { Id = s.Id }))
				.ForMember(t => t.Session, opt => opt.AddTransform(s => new Session { Id = s.Id } ));*/

			CreateMap<Ticket, TicketDTO>().ReverseMap();

			/*CreateMap<SessionDTO, Session>()
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.Movie, opt => opt.Ignore())
				.ForMember(dest => dest.Hall, opt => opt.Ignore());*/

			CreateMap<RegisterDTO, User>()
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User"));
		}
		
		private static DateTime ParseReleaseDate(string? releaseDate)
		{
			return DateTime.TryParse(releaseDate, out var date) ? date : DateTime.MinValue;
		}
	}
}