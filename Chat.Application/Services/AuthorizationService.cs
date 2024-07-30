using Chat.Application.Abstractions;
using Microsoft.AspNetCore.Identity;

using Chat.Domain.Dtos;
using Chat.Domain.Models;


namespace Chat.Application.Services;

public class AuthorizationService : IAuthorizationService
{
    
    
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
  

    public AuthorizationService(
        UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<UserIdDto> LoginUser(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user is null)
            throw new Exception("User doesn't exist");

        var result = await _signInManager
            .PasswordSignInAsync(user, loginDto.Password, false, false);

        var roles = await _userManager.GetRolesAsync(user);

        if (!result.Succeeded)
            throw new Exception("Incorrect login or password");

        await _userManager.UpdateAsync(user);

        var userIdDto = new UserIdDto
        {
            UserId = user.Id,
            Role = roles.FirstOrDefault()
        };

        return userIdDto;
    }

    public async Task<Guid> RegisterUser(RegistrationDto registrationDto)
    {
        string userName = registrationDto.Email.Substring(0, registrationDto.Email.IndexOf('@'));

        var user = new User()
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            Email = registrationDto.Email,
            FirstName = registrationDto.FirstName,
            SecondName = registrationDto.SecondName,
            
            
        };

        var result = await _userManager.CreateAsync(user, registrationDto.Password);

        if (!result.Succeeded)
            throw new Exception(result.Errors.First().Description);

        await _userManager.AddToRoleAsync(user, "User");
        
        return user.Id;

    }
    
    public Task Logout()
    {
        return _signInManager.SignOutAsync();
    }
}