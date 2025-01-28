using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.EntityModels;
using DataAccess.Models;

namespace BusinessLogic.Helpers
{
    public class MapperProfile : Profile
    {
		public MapperProfile()
		{
			CreateMap<Movie, MovieDTO>()
				.ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate.ToString("yyyy-MM-dd")))
				.ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
				.ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors));
		
			CreateMap<MovieDTO, Movie>()
				.ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => ParseReleaseDate(src.ReleaseDate)))
				.ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
				.ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors));
		
			CreateMap<Genre, GenreDTO>().ReverseMap();
		
			CreateMap<Actor, ActorDTO>().ReverseMap();
		}
		
		private static DateTime ParseReleaseDate(string? releaseDate)
		{
			return DateTime.TryParse(releaseDate, out var date) ? date : DateTime.MinValue;
		}
	}
}