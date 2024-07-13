using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;

namespace BookStoreApp.API.Configuration
{
	public class MappingConfig: Profile
	{
        public MappingConfig()
        {
            CreateMap<AuthorCreateDto, Author>();
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorUpdateDto, Author>();
        }
    }
}
