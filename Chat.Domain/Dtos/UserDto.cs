namespace Chat.Domain.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = string.Empty;

    public string SecondName { get; set; } = string.Empty;
}