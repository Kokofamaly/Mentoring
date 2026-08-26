using System.ComponentModel.DataAnnotations;

namespace WordCardsApi.Models
{
    public class UserWord
    {
        public string Id { get; set; }
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 1)]
        public string Word { get; set; }
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 1)]
        public string Translation { get; set; }
        [Required]
        public string UserId { get; set; }
        public int DifficultyLevel { get; set; } = 0;
        public string Language { get; set; }
        public string? Category { get; set; }
        public string? UsageExample { get; set; }
    }
}
