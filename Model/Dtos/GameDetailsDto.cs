using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dtos
{
    public record GameDetailsDto
    {
        public int PlayerId { get; set; }
        public string GameBoard { get; set; }
        public DateTime StaringTime { get; set; }
        public TimeSpan GameDuration { get; set; }

    }
}
