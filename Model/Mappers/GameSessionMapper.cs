using AutoMapper;
using Model.Boundaries;
using Model.Dtos;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Mappers
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
