using Amazon.SecretsManager.Model;
using Amazon.SecretsManager;
using CashFlowApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment.EnvironmentName;

if (environment.Equals("LocalDevelopment"))
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 31)))
    );
}
else
{
    DbSecret dbSecret = await LoadDatabaseSecretAsync();
    var connectionString = $"Server={dbSecret.Host};Port={dbSecret.Port};Database={dbSecret.Database};User Id={dbSecret.Username};Password={dbSecret.Password};";

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 31)))
    );
}

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

async Task<DbSecret> LoadDatabaseSecretAsync()
{
    using var client = new AmazonSecretsManagerClient();
    var request = new GetSecretValueRequest
    {
        SecretId = "CacheFlowDbSecret" 
    };

    var response = await client.GetSecretValueAsync(request);
    return JsonConvert.DeserializeObject<DbSecret>(response.SecretString);
}

public class DbSecret
{
    public string Host { get; set; }

    public string Port { get; set; }
    
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public string Database { get; set; }
}
