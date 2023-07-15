using System.ComponentModel.DataAnnotations;

namespace Model.bounderies
{
    public class PlayerBoundary
    {
        public int Id { get; init; }

        public string? FirstName { get; init; }

        public string? PhoneNumber { get; init; }
        public string? Country { get; init; }

    }
}
