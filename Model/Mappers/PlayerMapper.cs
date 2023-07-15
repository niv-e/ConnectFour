using AutoMapper;
using Model.bounderies;
using Model.Dtos;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Mappers
{
    public class PlayerMapper : Profile
    {
        public PlayerMapper()
        {
            CreateMap<Player, PlayerBoundary>();
            CreateMap<PlayerBoundary, Player>();

            CreateMap<Player, PlayerDto>();
            CreateMap<PlayerDto, Player>();

            CreateMap<PlayerDto, PlayerBoundary>();
            CreateMap<PlayerBoundary, PlayerDto>();

        }
    }
}
