using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlayerId { get; set; }

        [Required(ErrorMessage = "First name is a mandatory field")]
        [MinLength(2, ErrorMessage = "First name must have atleaste 2 characters")]
        public string FirstName { get; set; } = string.Empty;
        
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;
        
        [MinLength(1, ErrorMessage = "Country cannot set to empty string")]
        public string Country { get; set; } = string.Empty;
    }
}