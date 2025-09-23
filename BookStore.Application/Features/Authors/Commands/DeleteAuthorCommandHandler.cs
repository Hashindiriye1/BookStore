using BookStore.Domain.Common;
using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using MediatR;

namespace BookStore.Application.Features.Authors.Commands;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, OperationResult>
{
    private readonly IGenericRepository<Author> _authorRepository;

    public DeleteAuthorCommandHandler(IGenericRepository<Author> authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<OperationResult> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Check if author exists
            var existingAuthor = await _authorRepository.GetByIdAsync(request.Id);
            if (existingAuthor == null)
            {
                return OperationResult.Failure("Author not found");
            }

            await _authorRepository.DeleteAsync(request.Id);

            return OperationResult.Success("Author deleted successfully");
        }
        catch (Exception ex)
        {
            return OperationResult.Failure($"Error deleting author: {ex.Message}");
        }
    }
} 