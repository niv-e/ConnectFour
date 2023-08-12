using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dtos
{
    public record PlayerGamesCountDto
    {
        public string PlayerNameAndId { get; set; } = string.Empty;
        public int GamesCount { get; set; }
    }
}
