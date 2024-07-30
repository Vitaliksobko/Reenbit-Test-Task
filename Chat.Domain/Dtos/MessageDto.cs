using Azure.AI.TextAnalytics;

namespace Chat.Domain.Dtos;

public class MessageDto
{
    public string Text { get; set; } = null!;
    
    public DateTime Date { get; set; }
    
    public TextSentiment Sentiment { get; set; }
    public Guid UserId { get; set; }

    public UserDto? User { get; set; }
}