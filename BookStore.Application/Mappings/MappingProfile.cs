using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Domain.Entities;

namespace BookStore.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Author mappings
        CreateMap<Author, AuthorDto>();
        CreateMap<CreateAuthorDto, Author>();
        CreateMap<UpdateAuthorDto, Author>();

        // Book mappings
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
        CreateMap<CreateBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();
    }
} 