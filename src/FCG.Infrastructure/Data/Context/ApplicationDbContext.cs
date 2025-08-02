using FCG.Domain.Login;
using FCG.Domain.Game;
using FCG.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FCG.Infrastructure.Data.Context;

public class ApplicationDbContext(string connectionString) : DbContext
{
    public ApplicationDbContext(IConfiguration configuration) : this(configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found."))
    {
    }

    public DbSet<LoginModel> Logins { get; set; } = null!;
    public DbSet<UserModel> Users { get; set; } = null!;
    public DbSet<GameModel> Games { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
