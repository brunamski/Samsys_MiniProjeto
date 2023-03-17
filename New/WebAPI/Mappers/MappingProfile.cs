using AutoMapper;
using WebAPI.Infrastructure.DTOs;
using WebAPI.Infrastructure.Entities;

namespace WebAPI.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           //Authors

            CreateMap<AuthorDTO, Author>()
                .ForMember(dest => dest.authorId, opt => opt.Ignore());
            CreateMap<Author, AuthorDTO>();

            CreateMap<Author, GetAuthorsInfoDTO>();
            //CreateMap<List<Author>, List<GetAuthorsInfoDTO>>();

            //Books
            CreateMap<Book, BookDTO>()
    .ForMember(dest => dest.author, opt => opt.MapFrom(src => src.author.name))
    .ForMember(dest => dest.authorId, opt => opt.MapFrom(src => src.author.authorId));
            //CreateMap<Author, AuthorDTO>();

            CreateMap<AddBookDTO, Book>()
            .ForMember(dest => dest.author, opt => opt.Ignore());
            CreateMap<Book, AddBookDTO>();



        }
    }

}
