using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Chat.Api.Hubs;
using Chat.Application.Abstractions;
using Chat.Application.Models;
using Chat.Application.Services;
using Chat.Domain.Abstraction;
using Chat.Domain.Models;
using Chat.infrastructure;
using Chat.infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace Chat.Api;

public static class StartupHelper
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        
        var keyVaultUrl = builder.Configuration.GetSection("KeyVault:KeyVaultUrl");
        var keyVaultClientId = builder.Configuration.GetSection("KeyVault:ClientId");
        var keyVaultClientSecret = builder.Configuration.GetSection("KeyVault:ClientSecret");
        var keyVaultDirectoryId = builder.Configuration.GetSection("KeyVault:DirectoryId");
        
        
        var credential = new ClientSecretCredential(keyVaultDirectoryId.Value!,
            keyVaultClientId.Value!, keyVaultClientSecret.Value!);

        builder.Configuration.AddAzureKeyVault(keyVaultUrl.Value!, keyVaultClientId.Value!,
            keyVaultClientSecret.Value!, new DefaultKeyVaultSecretManager());
        
        var client = new SecretClient(new Uri(keyVaultUrl.Value!), credential);

        builder.Services.AddDbContext<ChatDbContext>(opt =>
            opt.UseNpgsql(client.GetSecret("DataBAseSecret").Value.Value));
        
        builder.Services.AddSignalR()
            .AddAzureSignalR(client.GetSecret("SignalRSecret").Value.Value);
        
        builder.Services.AddControllers();


        
        
        
        builder.Services.AddAutoMapper(typeof(MappingProfile.MappingProfile));
        builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IMessageRepository, MessageRepository>();


        builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IMessageService, MessageService>();
        builder.Services.AddScoped<IAnalysisService, AnalysisService>();

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped(typeof(Lazy<>), typeof(LazyInstance<>));


        builder.Services.AddSingleton<IDictionary<string, string>>(new Dictionary<string, string>());


         builder.Services.AddIdentity<User, Role>(options =>
                   {
                       options.Password.RequiredLength = 8;
                       options.Password.RequireNonAlphanumeric = false;
                       options.Password.RequireLowercase = false;
                       options.Password.RequireUppercase = false;
                       options.Password.RequireDigit = false;
                       options.User.RequireUniqueEmail = true;
                   })
                   .AddEntityFrameworkStores<ChatDbContext>()
                   .AddDefaultTokenProviders();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });
        

        builder.Services.AddAuthentication();

        return builder.Build();
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();


        app.UseCors("AllowSpecificOrigin");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapHub<ChatHub>("/chat");
        app.MapControllers();

        return app;
    }
}