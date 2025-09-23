using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Domain.Common;
using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using MediatR;

namespace BookStore.Application.Features.Authors.Queries;

public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, OperationResult<IEnumerable<AuthorDto>>>
{
    private readonly IGenericRepository<Author> _authorRepository;
    private readonly IMapper _mapper;

    public GetAllAuthorsQueryHandler(IGenericRepository<Author> authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<OperationResult<IEnumerable<AuthorDto>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var authors = await _authorRepository.GetAllAsync();
            var authorDtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);

            return OperationResult<IEnumerable<AuthorDto>>.Success(authorDtos, "Authors retrieved successfully");
        }
        catch (Exception ex)
        {
            return OperationResult<IEnumerable<AuthorDto>>.Failure($"Error retrieving authors: {ex.Message}");
        }
    }
} 