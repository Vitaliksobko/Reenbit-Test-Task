using Chat.Domain.Dtos;

namespace Chat.Application.Abstractions;

public interface IMessageService
{
    Task<List<MessageDto>> LoadMessages();

    Task SaveMessage(MessageDto userMessage);

    MessageDto CreateBotMessage(string text);
    Task<MessageDto> CreateUserMessage(string userId, string message);
    
}