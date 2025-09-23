using BookStore.Application.DTOs;
using BookStore.Application.Features.Authors.Commands;
using BookStore.Application.Features.Authors.Queries;
using BookStore.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<OperationResult<IEnumerable<AuthorDto>>>> GetAllAuthors()
    {
        var result = await _mediator.Send(new GetAllAuthorsQuery());
        
        if (result.IsSuccess)
            return Ok(result);
        
        return BadRequest(result);
    }

    [HttpPost]
    public async Task<ActionResult<OperationResult<AuthorDto>>> CreateAuthor([FromBody] CreateAuthorDto createAuthorDto)
    {
        var command = new CreateAuthorCommand
        {
            Name = createAuthorDto.Name,
            Email = createAuthorDto.Email,
            DateOfBirth = createAuthorDto.DateOfBirth
        };

        var result = await _mediator.Send(command);
        
        if (result.IsSuccess)
            return CreatedAtAction(nameof(GetAllAuthors), result);
        
        return BadRequest(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<OperationResult<AuthorDto>>> UpdateAuthor(int id, [FromBody] UpdateAuthorDto updateAuthorDto)
    {
        var command = new UpdateAuthorCommand
        {
            Id = id,
            Name = updateAuthorDto.Name,
            Email = updateAuthorDto.Email,
            DateOfBirth = updateAuthorDto.DateOfBirth
        };

        var result = await _mediator.Send(command);
        
        if (result.IsSuccess)
            return Ok(result);
        
        return BadRequest(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<OperationResult>> DeleteAuthor(int id)
    {
        var command = new DeleteAuthorCommand { Id = id };
        var result = await _mediator.Send(command);
        
        if (result.IsSuccess)
            return Ok(result);
        
        return BadRequest(result);
    }
} 