using BookStore.Domain.Common;
using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using MediatR;

namespace BookStore.Application.Features.Books.Commands;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, OperationResult>
{
    private readonly IGenericRepository<Book> _bookRepository;

    public DeleteBookCommandHandler(IGenericRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<OperationResult> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Check if book exists
            var existingBook = await _bookRepository.GetByIdAsync(request.Id);
            if (existingBook == null)
            {
                return OperationResult.Failure("Book not found");
            }

            await _bookRepository.DeleteAsync(request.Id);

            return OperationResult.Success("Book deleted successfully");
        }
        catch (Exception ex)
        {
            return OperationResult.Failure($"Error deleting book: {ex.Message}");
        }
    }
} 