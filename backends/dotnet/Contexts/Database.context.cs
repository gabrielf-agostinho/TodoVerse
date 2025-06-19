using Dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Contexts
{
  public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
  {
    public DbSet<Todo> Todos { get; set; }
  }
}