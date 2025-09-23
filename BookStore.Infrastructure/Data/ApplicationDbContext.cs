using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Author entity
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DateOfBirth).IsRequired();
        });

        // Configure Book entity
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.Property(e => e.PublishedDate).IsRequired();

            // Configure relationship
            entity.HasOne(e => e.Author)
                  .WithMany(a => a.Books)
                  .HasForeignKey(e => e.AuthorId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Seed some sample data
        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, Name = "John Doe", Email = "john@example.com", DateOfBirth = new DateTime(1980, 1, 1) },
            new Author { Id = 2, Name = "Jane Smith", Email = "jane@example.com", DateOfBirth = new DateTime(1975, 5, 15) }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "My First Book", Description = "A great book", Price = 19.99m, PublishedDate = new DateTime(2020, 1, 1), AuthorId = 1 },
            new Book { Id = 2, Title = "Another Book", Description = "Another great book", Price = 29.99m, PublishedDate = new DateTime(2021, 6, 1), AuthorId = 2 }
        );
    }
} 