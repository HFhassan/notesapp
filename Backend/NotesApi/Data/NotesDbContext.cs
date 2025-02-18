using Microsoft.EntityFrameworkCore;
using NotesApi.Models;

namespace NotesApi.Data;

public class NotesDbContext : DbContext
{
    public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options)
    {
    }

    public DbSet<Note> Notes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>()
            .Property(n => n.Title)
            .IsRequired();

        modelBuilder.Entity<Note>()
            .Property(n => n.Content)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
} 