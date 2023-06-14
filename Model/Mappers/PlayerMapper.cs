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
            CreateMap<PlayerBoundary, Player>();
            CreateMap<Player, PlayerBoundary>();
            CreateMap<Player, PlayerDto>();
        }
    }
}
