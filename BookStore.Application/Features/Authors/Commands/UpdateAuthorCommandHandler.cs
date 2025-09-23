using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Domain.Common;
using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using MediatR;

namespace BookStore.Application.Features.Authors.Commands;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, OperationResult<AuthorDto>>
{
    private readonly IGenericRepository<Author> _authorRepository;
    private readonly IMapper _mapper;

    public UpdateAuthorCommandHandler(IGenericRepository<Author> authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<OperationResult<AuthorDto>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Check if author exists
            var existingAuthor = await _authorRepository.GetByIdAsync(request.Id);
            if (existingAuthor == null)
            {
                return OperationResult<AuthorDto>.Failure("Author not found");
            }

            // Simple validation
            if (string.IsNullOrEmpty(request.Name))
            {
                return OperationResult<AuthorDto>.Failure("Author name is required");
            }

            if (string.IsNullOrEmpty(request.Email))
            {
                return OperationResult<AuthorDto>.Failure("Author email is required");
            }

            // Update author properties
            existingAuthor.Name = request.Name;
            existingAuthor.Email = request.Email;
            existingAuthor.DateOfBirth = request.DateOfBirth;

            var updatedAuthor = await _authorRepository.UpdateAsync(existingAuthor);
            var authorDto = _mapper.Map<AuthorDto>(updatedAuthor);

            return OperationResult<AuthorDto>.Success(authorDto, "Author updated successfully");
        }
        catch (Exception ex)
        {
            return OperationResult<AuthorDto>.Failure($"Error updating author: {ex.Message}");
        }
    }
} 