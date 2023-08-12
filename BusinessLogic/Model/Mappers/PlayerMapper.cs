using AutoMapper;
using BusinessLogic.Model.Boundaries;
using BusinessLogic.Model.Dtos;
using DAL.Entities;

namespace BusinessLogic.Model.Mappers
{
    public class PlayerMapper : Profile
    {
        public PlayerMapper()
        {
            CreateMap<Player, PlayerBoundary>();
            CreateMap<PlayerBoundary, Player>()
                .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.PlayerId));
            // Map the Id property explicitly

            CreateMap<Player, PlayerDto>();
            CreateMap<PlayerDto, Player>();

            CreateMap<PlayerDto, PlayerBoundary>();
            CreateMap<PlayerBoundary, PlayerDto>();

        }
    }
}
