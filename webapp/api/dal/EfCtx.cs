using Microsoft.EntityFrameworkCore;
using domain;

namespace dal;
public class EfCtx : DbContext
{
    public DbSet<Animal> Animals { get; set; }

    public EfCtx(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // this not working in memory, accept my apology
        // I found that sqllite in memory could work with that, but I don't wanna use it
        modelBuilder.Entity<Animal>()
            .HasIndex(a => a.Name)
            .IsUnique();
    }
}

