namespace Model.Entities
{
    public class Player
    {
        public int Id { get; init; }

        public string FirstName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;
    }
}