

using Chat.Domain.Abstraction;

namespace Chat.infrastructure.Repositories;

public class UnitOfWork(
    ChatDbContext context,
   
    Lazy<IUserRepository> userRepository,
    Lazy<IMessageRepository> messageRepository) : IUnitOfWork
{
    

    public IUserRepository User => userRepository.Value;

    public IMessageRepository Message => messageRepository.Value;
    public async Task SaveAsync() => await  context.SaveChangesAsync();
}