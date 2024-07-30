namespace Chat.Domain.Abstraction;

public interface IUnitOfWork
{
    IUserRepository User { get; }
    
    IMessageRepository Message { get; }


    Task SaveAsync();
}