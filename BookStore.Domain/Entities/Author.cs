namespace BookStore.Domain.Entities;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    
    // Navigation property for books written by this author
    public List<Book> Books { get; set; } = new List<Book>();
} 