using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.bounderies
{
    public record PlayerForCreationDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        public string FirstName { get; init; }

        public PlayerForCreationDto(string firstName)
        {
            FirstName = firstName;
        }

    }
}
