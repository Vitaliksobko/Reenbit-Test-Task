using Chat.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Chat.Domain.Models;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;

    public string SecondName { get; set; } = string.Empty;

    public List<Message> Messages { get; set; } = [];
}