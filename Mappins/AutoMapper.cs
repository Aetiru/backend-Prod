using Backend.Contracts;
using Backend.Models;
using AutoMapper;
using System.Text.Json;


namespace Backend.Mappins
{
    public class AutoMapperRick : Profile
    {
        public AutoMapperRick()
        {
            CreateMap<CreateRickRequest, Rick>()
           .ForMember(dest => dest.OriginJson, opt => opt.MapFrom(src => SerializeToJson(src.Origin)))
           .ForMember(dest => dest.LocationJson, opt => opt.MapFrom(src => SerializeToJson(src.Location)))
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Episodes, opt => opt.MapFrom(src => src.Episodes))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.url));

            CreateMap<UpdateRickRequest, Rick>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            
            

            CreateMap<Location, string>().ConvertUsing(src => SerializeToJson(src));

        }
        private static string SerializeToJson<T>(T value)
        {
            return JsonSerializer.Serialize(value);
        }
    }

}