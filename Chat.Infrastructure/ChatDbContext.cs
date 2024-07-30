using Chat.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace Chat.infrastructure;

public class ChatDbContext : IdentityDbContext<User, Role, Guid>
{
    
    public DbSet<Message> Message { get; set; }
    
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {
        
    }
    
   
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>()
            .HasData(
                new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = "User",
                    NormalizedName = "USER"
                }); 
        
    }
}