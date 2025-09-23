namespace BookStore.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime PublishedDate { get; set; }
    
    // Foreign key for Author
    public int AuthorId { get; set; }
    
    // Navigation property to Author
    public Author Author { get; set; } = null!;
} 