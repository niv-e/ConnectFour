using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Model.Boundaries
{
    public class PlayerBoundary
    {
        //[IdExists("m", ErrorMessage = "First name must have atleaste 2 characters")]
        public int PlayerId { get; init; }

        [Required(ErrorMessage = "First name is a mandatory field")]
        [MinLength(2, ErrorMessage = "First name must have atleaste 2 characters")]
        public string? FirstName { get; init; }

        [Phone(ErrorMessage = "Invalid phone number")]
        public string? PhoneNumber { get; init; }

        [MinLength(1, ErrorMessage = "Country cannot set to empty string")]
        public string? Country { get; init; }

    }
}
