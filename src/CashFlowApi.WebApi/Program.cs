using Amazon.SecretsManager.Model;
using Amazon.SecretsManager;
using CashFlowApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using Amazon.CloudWatchLogs;
using Serilog.Sinks.AwsCloudWatch;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment.EnvironmentName;

builder.Logging.ClearProviders();

var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console();

if (environment.Equals("LocalDevelopment"))
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 31)))
    );
}
else
{
    var awsOptions = builder.Configuration.GetAWSOptions();
    var cloudWatchLogsClient = awsOptions.CreateServiceClient<IAmazonCloudWatchLogs>();
    var cloudWatchClient = new AmazonCloudWatchLogsClient();

    logger.WriteTo.AmazonCloudWatch(
        new CloudWatchSinkOptions
        {
            LogGroupName = Environment.GetEnvironmentVariable("CLOUD_WATCH__LOG_GROUP_NAME"),
            CreateLogGroup = true,
            LogStreamNameProvider = new CustomLogStreamNameProvider(),
            BatchSizeLimit = 1000,
            Period = TimeSpan.FromSeconds(10),
            TextFormatter = new CompactJsonFormatter()
        },
        cloudWatchClient
    );

    DbSecret dbSecret = await LoadDatabaseSecretAsync();
    var connectionString = $"Server={dbSecret.Host};Port={dbSecret.Port};Database={dbSecret.Database};User Id={dbSecret.Username};Password={dbSecret.Password};";

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 31)))
    );
}

Log.Logger = logger.CreateLogger();
builder.Logging.AddSerilog();

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
    var request = new GetSecretValueRequest { SecretId = Environment.GetEnvironmentVariable("SECRET_NAME__DB_CREDENTIALS") };

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

public class CustomLogStreamNameProvider : ILogStreamNameProvider
{
    public string GetLogStreamName()
    {
        return
            $"{Environment.GetEnvironmentVariable("CLOUD_WATCH__LOG_STREAM_NAME")}-" +
            $"{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLowerInvariant()}-" +
            $"{DateTime.UtcNow:yyyyMMdd}";
    }
}
