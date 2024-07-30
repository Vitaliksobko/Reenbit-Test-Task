
using Chat.Domain.Abstraction;
using Chat.Domain.Models;


namespace Chat.infrastructure.Repositories;

public class UserRepository(ChatDbContext context)
    : BaseRepository<User>(context), IUserRepository
{
   
}