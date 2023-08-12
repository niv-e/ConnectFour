using AutoMapper;
using BusinessLogic.Model.Boundaries;
using BusinessLogic.Model.Dtos;
using DAL.Entities;

namespace BusinessLogic.Model.Mappers
{
    internal class GameSessionMapper : Profile
    {
        public GameSessionMapper()
        {
            CreateMap<GameSession, GameSessionDto>();
            CreateMap<GameSessionDto, GameSession>();
            CreateMap<GameSessionDto, GameSessionBoundary>();
        }
    }
}
