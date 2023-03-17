using AutoMapper;
using WebAPI.Infrastructure.Entities;

namespace WebAPI.Infrastructure.DTOs
{

    //GET Methods from Book, with Author Name from Author
    public class BookListToBookDetailsDtoListConverter : ITypeConverter<List<Book>, List<BookDetailsDTO>>
    {
        public List<BookDetailsDTO> Convert(List<Book> source, List<BookDetailsDTO> destination, ResolutionContext context)
        {
            var result = new List<BookDetailsDTO>();
            foreach (var book in source)
            {
                var bookDto = context.Mapper.Map<BookDetailsDTO>(book);
                result.Add(bookDto);
            }
            return result;
        }
    }

}
