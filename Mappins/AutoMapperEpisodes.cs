using Backend.Contracts;
using Backend.Models;
using AutoMapper;
using System.Text.Json;


namespace Backend.Mappins
{
    public class AutoMapperEpisodes : Profile
    {
        public AutoMapperEpisodes()
        {
            CreateMap<CreateEpisodeRequest, Episode>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Characters, opt => opt.MapFrom(src => src.Characters))
                .ForMember(dest => dest.EpisodeCode, opt => opt.MapFrom(src => src.EpisodeCode))
                .ForMember(dest => dest.AirDate, opt => opt.MapFrom(src => src.AirDate))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));

            // Mapeo para la actualización de episodios
            CreateMap<UpdateEpisodeRequest, Episode>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore()); // No se debe actualizar la fecha de creación



            CreateMap<Location, string>().ConvertUsing(src => SerializeToJson(src));

        }
        private static string SerializeToJson<T>(T value)
        {
            return JsonSerializer.Serialize(value);
        }
    }

}