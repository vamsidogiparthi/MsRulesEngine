using Microsoft.EntityFrameworkCore;
using MsRulesEngine.DBConfigurations;
namespace MsRulesEngine.DBContext;

public class ArcadeDbContext : DbContext
{
    public string DbPath { get; private set; }
    public ArcadeDbContext()
    {
        DbPath =  Path.Combine(Environment.CurrentDirectory, "Database", "ArcadeDb.db");
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<Arcade> Arcades { get; set; }

    #region Required
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArcadeDbConfiguration).Assembly);
        string filePath = Path.Combine(Environment.CurrentDirectory, "RulesFiles", "DiscountRules.json");
        string jsonString = File.ReadAllText(filePath);

        Arcade arcade = new()
        {
            Name = "Arcade1",
            Id = 1,
            Workflows = jsonString
        };

        Game game = new()
        {
            Title = "Halo",
            Genre = "Action",
            Id = 1,
            Platform = "PlayStation",
            GamingStudio = "Sony",
            Price = 59.99m
        };

        modelBuilder.Entity<Game>().HasData(game);
        modelBuilder.Entity<Arcade>().HasData(arcade);

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }

    #endregion

}