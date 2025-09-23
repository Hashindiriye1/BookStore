using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Domain.Common;
using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using MediatR;

namespace BookStore.Application.Features.Books.Commands;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, OperationResult<BookDto>>
{
    private readonly IGenericRepository<Book> _bookRepository;
    private readonly IGenericRepository<Author> _authorRepository;
    private readonly IMapper _mapper;

    public UpdateBookCommandHandler(IGenericRepository<Book> bookRepository, IGenericRepository<Author> authorRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<OperationResult<BookDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Check if book exists
            var existingBook = await _bookRepository.GetByIdAsync(request.Id);
            if (existingBook == null)
            {
                return OperationResult<BookDto>.Failure("Book not found");
            }

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

            // Update book properties
            existingBook.Title = request.Title;
            existingBook.Description = request.Description;
            existingBook.Price = request.Price;
            existingBook.PublishedDate = request.PublishedDate;
            existingBook.AuthorId = request.AuthorId;

            var updatedBook = await _bookRepository.UpdateAsync(existingBook);
            updatedBook.Author = author; // Set for mapping
            
            var bookDto = _mapper.Map<BookDto>(updatedBook);

            return OperationResult<BookDto>.Success(bookDto, "Book updated successfully");
        }
        catch (Exception ex)
        {
            return OperationResult<BookDto>.Failure($"Error updating book: {ex.Message}");
        }
    }
} 