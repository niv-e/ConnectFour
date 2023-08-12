using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dtos
{
    public record PlayerLastGameDto
    {
        public string PlayerNameAndId { get; set; } = string.Empty;
        public DateTime LastGame { get; set; }
    }
}
