using asp.netCoreWebApi.models;
using asp.netCoreWebApi.models.Dto;
using asp.netCoreWebApi.models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace asp.netCoreWebApi
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {

            // player mapper
            CreateMap<Player, PlayerDTO>();
            CreateMap<PlayerDTO, Player>();

            CreateMap<Player, PlayerCreateDTO>().ReverseMap();
            CreateMap<Player, PlayerUpdateDTO>().ReverseMap();

            //CreateMap<CreatedAtRouteResult, PlayerCreateDTO>().ReverseMap();

            // player number 
            CreateMap<PlayerNumber, PlayerNumberDTO>().ReverseMap();
            CreateMap<PlayerNumber, PlayerNumberCreateDTO>().ReverseMap();
            CreateMap<PlayerNumber, PlayerNumberUpdateDTO>().ReverseMap();
        }
    }
}
    