using Microsoft.EntityFrameworkCore;
using TechLibrary.Api.Domain.Entities;

namespace TechLibrary.Api.Infraestructure.DataAcess;

public class TechLibraryDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var solutionDir = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName;
        if (solutionDir is not null)
        {
            var dbPath = Path.Combine(solutionDir, "TechLibraryDb.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");            
        }
    }
}
