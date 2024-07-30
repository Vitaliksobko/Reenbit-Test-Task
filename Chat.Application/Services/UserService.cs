using AutoMapper;
using Chat.Application.Abstractions;
using Chat.Domain.Abstraction;
using Chat.Domain.Dtos;

namespace Chat.Application.Services;

public class UserService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IUserService
{
    public async Task<UserDto> GetUserById(Guid userId)
    {
        var user = await unitOfWork.User
            .GetSingleByConditionAsync(u => u.Id == userId);
        
        return mapper.Map<UserDto>(user);
    }
    
    
    public async Task<List<UserDto>> GetUsersByIdsAsync(IEnumerable<Guid> userIds)
    {

        
        var users = await unitOfWork.User
            .GetByConditionsAsync(u => userIds.Contains(u.Id));

        return mapper.Map<List<UserDto>>(users);
    }

}