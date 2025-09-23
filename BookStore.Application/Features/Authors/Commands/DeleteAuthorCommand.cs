using BookStore.Domain.Common;
using MediatR;

namespace BookStore.Application.Features.Authors.Commands;

public class DeleteAuthorCommand : IRequest<OperationResult>
{
    public int Id { get; set; }
} 