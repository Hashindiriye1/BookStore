using BookStore.Application.DTOs;
using BookStore.Domain.Common;
using MediatR;

namespace BookStore.Application.Features.Books.Queries;

public class GetAllBooksQuery : IRequest<OperationResult<IEnumerable<BookDto>>>
{
} 