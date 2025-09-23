using BookStore.Application.DTOs;
using BookStore.Application.Features.Books.Commands;
using BookStore.Application.Features.Books.Queries;
using BookStore.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<OperationResult<IEnumerable<BookDto>>>> GetAllBooks()
    {
        var result = await _mediator.Send(new GetAllBooksQuery());
        
        if (result.IsSuccess)
            return Ok(result);
        
        return BadRequest(result);
    }

    [HttpPost]
    public async Task<ActionResult<OperationResult<BookDto>>> CreateBook([FromBody] CreateBookDto createBookDto)
    {
        var command = new CreateBookCommand
        {
            Title = createBookDto.Title,
            Description = createBookDto.Description,
            Price = createBookDto.Price,
            PublishedDate = createBookDto.PublishedDate,
            AuthorId = createBookDto.AuthorId
        };

        var result = await _mediator.Send(command);
        
        if (result.IsSuccess)
            return CreatedAtAction(nameof(GetAllBooks), result);
        
        return BadRequest(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<OperationResult<BookDto>>> UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
    {
        var command = new UpdateBookCommand
        {
            Id = id,
            Title = updateBookDto.Title,
            Description = updateBookDto.Description,
            Price = updateBookDto.Price,
            PublishedDate = updateBookDto.PublishedDate,
            AuthorId = updateBookDto.AuthorId
        };

        var result = await _mediator.Send(command);
        
        if (result.IsSuccess)
            return Ok(result);
        
        return BadRequest(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<OperationResult>> DeleteBook(int id)
    {
        var command = new DeleteBookCommand { Id = id };
        var result = await _mediator.Send(command);
        
        if (result.IsSuccess)
            return Ok(result);
        
        return BadRequest(result);
    }
} 