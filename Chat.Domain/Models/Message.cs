using System.Text.Json.Serialization;
using Azure.AI.TextAnalytics;


namespace Chat.Domain.Models;

public class Message
{
    public Guid Id { get; set; }

    public string Text { get; set; } = null!;
    
    public DateTime Date { get; set; }

    public TextSentiment Sentiment { get; set; }
    public Guid UserId { get; set; }

    [JsonIgnore]
    public User? User { get; set; }
}