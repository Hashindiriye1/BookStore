using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Domain.Common;
using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using MediatR;

namespace BookStore.Application.Features.Books.Queries;

public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, OperationResult<IEnumerable<BookDto>>>
{
    private readonly IGenericRepository<Book> _bookRepository;
    private readonly IGenericRepository<Author> _authorRepository;
    private readonly IMapper _mapper;

    public GetAllBooksQueryHandler(IGenericRepository<Book> bookRepository, IGenericRepository<Author> authorRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<OperationResult<IEnumerable<BookDto>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var books = await _bookRepository.GetAllAsync();
            var authors = await _authorRepository.GetAllAsync();
            
            // Simple way to include author information for beginners
            var booksWithAuthors = books.Select(book =>
            {
                book.Author = authors.FirstOrDefault(a => a.Id == book.AuthorId) ?? new Author { Name = "Unknown" };
                return book;
            });

            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(booksWithAuthors);

            return OperationResult<IEnumerable<BookDto>>.Success(bookDtos, "Books retrieved successfully");
        }
        catch (Exception ex)
        {
            return OperationResult<IEnumerable<BookDto>>.Failure($"Error retrieving books: {ex.Message}");
        }
    }
} 