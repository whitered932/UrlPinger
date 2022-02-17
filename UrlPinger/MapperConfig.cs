using AutoMapper;
using UrlPinger.Models;
using UrlPinger.Models.Dto;

namespace UrlPinger
{
    public class MapperConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<RemoteAddressDto, RemoteAddress>();
                config.CreateMap<RemoteAddress, RemoteAddressDto>();
            });
        }
    }
}
