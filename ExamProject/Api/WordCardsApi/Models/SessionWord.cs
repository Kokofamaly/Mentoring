using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WordCardsApi.Models;

public class SessionWord
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [Required]
    public string SessionId { get; set; }
    [Required]
    public string UserWordId { get; set; }
    public bool? isCorrect { get; set; }
    [Required]
    public string Word { get; set; }
    [Required]
    public string Translation { get; set; }
    public string? UsageExample { get; set; }
    public int Order { get; set; } = 0;
}