

using Chat.Domain.Dtos;

namespace Chat.Application.Abstractions;

public interface IAuthorizationService
{
    Task<UserIdDto> LoginUser(LoginDto loginDto);
    Task<Guid> RegisterUser(RegistrationDto registrationDto);

    Task Logout();
}