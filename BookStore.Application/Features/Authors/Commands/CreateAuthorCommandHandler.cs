using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Domain.Common;
using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using MediatR;

namespace BookStore.Application.Features.Authors.Commands;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, OperationResult<AuthorDto>>
{
    private readonly IGenericRepository<Author> _authorRepository;
    private readonly IMapper _mapper;

    public CreateAuthorCommandHandler(IGenericRepository<Author> authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<OperationResult<AuthorDto>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Simple validation
            if (string.IsNullOrEmpty(request.Name))
            {
                return OperationResult<AuthorDto>.Failure("Author name is required");
            }

            if (string.IsNullOrEmpty(request.Email))
            {
                return OperationResult<AuthorDto>.Failure("Author email is required");
            }

            // Create new author
            var author = new Author
            {
                Name = request.Name,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth
            };

            var createdAuthor = await _authorRepository.AddAsync(author);
            var authorDto = _mapper.Map<AuthorDto>(createdAuthor);

            return OperationResult<AuthorDto>.Success(authorDto, "Author created successfully");
        }
        catch (Exception ex)
        {
            return OperationResult<AuthorDto>.Failure($"Error creating author: {ex.Message}");
        }
    }
} 