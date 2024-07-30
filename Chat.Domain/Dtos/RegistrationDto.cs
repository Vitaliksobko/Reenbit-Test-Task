namespace Chat.Domain.Dtos;

public class RegistrationDto
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    
    public string FirstName { get; set; } = string.Empty;

    public string SecondName { get; set; } = string.Empty;
}