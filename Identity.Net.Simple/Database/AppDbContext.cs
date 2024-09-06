using Identity.Net.Simple.Models.DB;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Net.Simple.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) :
    IdentityDbContext<User>(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.HasDefaultSchema(Schemas.Default);

        base.OnModelCreating(modelBuilder);
    }

}