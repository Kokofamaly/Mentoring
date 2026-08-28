using System.ComponentModel.DataAnnotations;

namespace WordCardsApi.Models
{
    public class User
    {
        public string Id { get; set; }
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string HashedPassword { get; set; }
        public string Role { get; set; } = "User";

    }
}
