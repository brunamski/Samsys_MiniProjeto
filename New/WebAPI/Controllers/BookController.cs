using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;
using WebAPI.Infrastructure.DTOs;
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
        public async Task<MessagingHelper<List<BookDTO>>> GetAll()
        {
            return await _service.GetLivros();
        }

        [HttpGet]
        [Route("livros/{isbn}")]
        public async Task<MessagingHelper<List<BookDTO>>> GetBookByISBN(string isbn)
        {
            return await _service.GetLivro(isbn);
        }

        
        [HttpPost]
        [Route("criarLivro")]
        
        public async Task<MessagingHelper<List<AddBookDTO>>> AddLivro(AddBookDTO objLivro)
        {
            return await _service.AddLivro(objLivro);
        }
        
        [HttpPatch]
        [Route("atualizarLivro/{isbn}")]
        public async Task<MessagingHelper<List<AddBookDTO>>> UpdateLivro(string isbn, [FromBody] AddBookDTO livroToUpdate)
        {
            return await _service.UpdateLivro(isbn, livroToUpdate);
        }



        
        [HttpDelete]
        [Route("apagarLivro/{isbn}")]
        public async Task<MessagingHelper<List<AddBookDTO>>> DeleteLivro(string isbn)
        {
            return await _service.DeleteLivro(isbn);
        }
    }
}
