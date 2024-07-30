using Chat.Domain.Dtos;

namespace Chat.Application.Abstractions;

public interface IUserService
{
    Task<UserDto> GetUserById(Guid userId);

    Task<List<UserDto>> GetUsersByIdsAsync(IEnumerable<Guid> userIds);

}