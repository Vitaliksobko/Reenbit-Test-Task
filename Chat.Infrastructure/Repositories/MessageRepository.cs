using System.Linq.Expressions;
using Chat.Domain.Abstraction;
using Chat.Domain.Models;

namespace Chat.infrastructure.Repositories;

public class MessageRepository(ChatDbContext context)
    : BaseRepository<Message>(context), IMessageRepository

{
 
}