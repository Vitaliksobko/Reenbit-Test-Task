using AutoMapper;
using Chat.Application.Abstractions;
using Chat.Domain.Abstraction;
using Chat.Domain.Dtos;
using Chat.Domain.Models;

namespace Chat.Application.Services;

public class MessageService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IAnalysisService analysisService) : IMessageService
{
    
    public MessageDto CreateBotMessage(string text)
    {
        return new MessageDto()
        {
            Text = text,
            Date = DateTime.Now.ToUniversalTime(),
            UserId = Guid.Empty,
            User = new UserDto() { FirstName = "Chat", SecondName = "bot" }
        };
    }

    public async Task<MessageDto> CreateUserMessage(string userId, string message)
    {
        var parsedUserId = Guid.Parse(userId);
        
        var user = await unitOfWork.User.GetSingleByConditionAsync(u => u.Id == parsedUserId);
        var userDto = mapper.Map<UserDto>(user);
        var sentiment = analysisService.MessageAnalysis(message);
        return new MessageDto()
        {
            Text = message,
            Date = DateTime.Now.ToUniversalTime(),
            Sentiment = sentiment ,
            UserId = parsedUserId,
            User = userDto
        };
    }
    
    public async Task<List<MessageDto>> LoadMessages()
    {
        var messages = await unitOfWork.Message.GetAll();
        return messages.Select(mapper.Map<MessageDto>).ToList();
    }
    
    public async Task SaveMessage(MessageDto userMessage)
    {
        var message = new Message()
        {
            Text = userMessage.Text,
            Sentiment = userMessage.Sentiment,
            Date = userMessage.Date,
            UserId = userMessage.UserId
        };
        await unitOfWork.Message.InsertAsync(message);
        await unitOfWork.SaveAsync();
    }
    
}