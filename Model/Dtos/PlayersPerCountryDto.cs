using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dtos
{
    public record PlayersPerCountryDto
    {
        public string Country { get; set; } = string.Empty;
        public IEnumerable<string> PlayerNameAndId { get; set; } = Enumerable.Empty<string>();
    }
}
