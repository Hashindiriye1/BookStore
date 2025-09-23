using BookStore.Application.DTOs;
using BookStore.Domain.Common;
using MediatR;

namespace BookStore.Application.Features.Books.Commands;

public class UpdateBookCommand : IRequest<OperationResult<BookDto>>
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime PublishedDate { get; set; }
    public int AuthorId { get; set; }
} 