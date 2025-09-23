using BookStore.Application.DTOs;
using BookStore.Domain.Common;
using MediatR;

namespace BookStore.Application.Features.Authors.Commands;

public class UpdateAuthorCommand : IRequest<OperationResult<AuthorDto>>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
} 