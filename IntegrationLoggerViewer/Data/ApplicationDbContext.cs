using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace IntegrationLoggerViewer.Data;
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName != null && tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName($"LoggerViewer_{tableName.Substring(6)}");
            }
        }
    }
}

/*
 dotnet ef migrations add NewScripts --context ApplicationDbContext --startup-project ..\IntegrationLoggerViewer\
 dotnet ef migrations script -o ./Script/script.sql --context DataContext
 */
