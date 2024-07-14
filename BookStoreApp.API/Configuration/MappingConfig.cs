using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using BookStoreApp.API.Models.Book;
using BookStoreApp.API.Models.User;

namespace BookStoreApp.API.Configuration
{
	public class MappingConfig: Profile
	{
        public MappingConfig()
        {
            CreateMap<AuthorCreateDto, Author>();
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorUpdateDto, Author>();

            CreateMap<Book, BookDto>()
                .ForMember(x => x.AuthorName, opt => opt.MapFrom(x => $"{x.Author.FirstName} {x.Author.LastName}" ));
            CreateMap<BookCreateDto, Book>();
            CreateMap<BookUpdateDto, Book>();
            CreateMap<Book, BookDetailsDto>()
                .ForMember(x => x.AuthorName, opt => opt.MapFrom(x => $"{x.Author.FirstName} {x.Author.LastName}" ));

            CreateMap<UserDto, ApplicationUser>();
        }
    }
}
