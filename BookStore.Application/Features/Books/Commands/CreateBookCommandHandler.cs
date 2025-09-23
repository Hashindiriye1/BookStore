using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Domain.Common;
using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using MediatR;

namespace BookStore.Application.Features.Books.Commands;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, OperationResult<BookDto>>
{
    private readonly IGenericRepository<Book> _bookRepository;
    private readonly IGenericRepository<Author> _authorRepository;
    private readonly IMapper _mapper;

    public CreateBookCommandHandler(IGenericRepository<Book> bookRepository, IGenericRepository<Author> authorRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<OperationResult<BookDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Simple validation
            if (string.IsNullOrEmpty(request.Title))
            {
                return OperationResult<BookDto>.Failure("Book title is required");
            }

            // Check if author exists
            var author = await _authorRepository.GetByIdAsync(request.AuthorId);
            if (author == null)
            {
                return OperationResult<BookDto>.Failure("Author not found");
            }

            // Create new book
            var book = new Book
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                PublishedDate = request.PublishedDate,
                AuthorId = request.AuthorId
            };

            var createdBook = await _bookRepository.AddAsync(book);
            createdBook.Author = author; // Set for mapping
            
            var bookDto = _mapper.Map<BookDto>(createdBook);

            return OperationResult<BookDto>.Success(bookDto, "Book created successfully");
        }
        catch (Exception ex)
        {
            return OperationResult<BookDto>.Failure($"Error creating book: {ex.Message}");
        }
    }
} 