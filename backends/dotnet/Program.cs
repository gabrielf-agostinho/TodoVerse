using Dotnet.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var localDatabasePath = "Database/todoverse.db";
var databasePath = Environment.GetEnvironmentVariable("DATABASE_PATH") ?? localDatabasePath;

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite($"Data Source={databasePath}"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (databasePath == localDatabasePath)
{
  var dir = Path.GetDirectoryName(localDatabasePath);

  if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
    Directory.CreateDirectory(dir);

  using var scope = app.Services.CreateScope();
  var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
  db.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
var url = isDocker ? "http://0.0.0.0:80" : "http://localhost:5000";

app.Run(url);