using Microsoft.EntityFrameworkCore;

namespace FinBeatDemo.Models;

public class FinBeatDemoContext : DbContext
{
    public DbSet<Item> Items { get; set; }
    public string DbPath { get; }

    public FinBeatDemoContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "fin_beat_demo.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseLazyLoadingProxies()
        .UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
            .Property(et => et.Id)
            .ValueGeneratedNever();
    }
}