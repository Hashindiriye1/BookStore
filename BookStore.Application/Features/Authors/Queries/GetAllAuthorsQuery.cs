using BookStore.Application.DTOs;
using BookStore.Domain.Common;
using MediatR;

namespace BookStore.Application.Features.Authors.Queries;

public class GetAllAuthorsQuery : IRequest<OperationResult<IEnumerable<AuthorDto>>>
{
} 