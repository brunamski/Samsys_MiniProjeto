using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;
using WebAPI.Infrastructure.Entities;
using WebAPI.Infrastructure.Helpers;
using WebAPI.Infrastructure.Services;

namespace WebAPI.Controllers
{
    [Route("api/Livro")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly BookService _service;
        public BookController(BookService service)
        {
            _service = service;
        }



        [HttpGet]
        [Route("livros")]
        public async Task<MessagingHelper<List<Book>>> GetAll()
        {
            return await _service.GetLivros();
        }

        [HttpGet]
        [Route("livros/{isbn}")]
        public async Task<MessagingHelper<List<Book>>> GetBookByISBN(string isbn)
        {
            return await _service.GetLivro(isbn);
        }

        
        [HttpPost]
        [Route("criarLivro")]
        
        public async Task<MessagingHelper<List<Book>>> AddLivro(Book objLivro)
        {
            return await _service.AddLivro(objLivro);
        }
        
        [HttpPatch]
        [Route("atualizarLivro/{isbn}")]
        public async Task<MessagingHelper<List<Book>>> UpdateLivro(string isbn, [FromBody] Book livroToUpdate)
        {
            return await _service.UpdateLivro(isbn, livroToUpdate);
        }



        
        [HttpDelete]
        [Route("apagarLivro/{isbn}")]
        public async Task<MessagingHelper<List<Book>>> DeleteLivro(string isbn)
        {
            return await _service.DeleteLivro(isbn);
        }
    }
}
