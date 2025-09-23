using BookStore.Domain.Common;
using MediatR;

namespace BookStore.Application.Features.Books.Commands;

public class DeleteBookCommand : IRequest<OperationResult>
{
    public int Id { get; set; }
} 