using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;
using WebAPI.Infrastructure.DTOs;
using WebAPI.Infrastructure.Entities;
using WebAPI.Infrastructure.Helpers;
using WebAPI.Infrastructure.Services;

namespace WebAPI.Controllers
{
    [Route("api/Autor")]
    [ApiController]
    public class AuthorController : ControllerBase
    {

        private readonly AuthorService _service;
        public AuthorController(AuthorService service)
        {
            _service = service;
        }



        [HttpGet]
        [Route("autores")]
        public async Task<MessagingHelper<List<Author>>> GetAll()
        {
            return await _service.GetAuthors();
        }

        [HttpGet]
        [Route("autores/{authorId}")]
        public async Task<MessagingHelper<List<Author>>> GetAuthorByName(long authorId)
        {
            return await _service.GetAuthor(authorId);
        }


        [HttpPost]
        [Route("criarAutor")]

        public async Task<MessagingHelper<List<AuthorDTO>>> AddAuthor(AuthorDTO objAuthor)
        {
            return await _service.AddAuthor(objAuthor);
        }

        [HttpPatch]
        [Route("atualizarAutor/{authorId}")]
        public async Task<MessagingHelper<List<Author>>> UpdateAuthor(long authorId, [FromBody] Author authorToUpdate)
        {
            return await _service.UpdateAuthor(authorId, authorToUpdate);
        }




        [HttpDelete]
        [Route("apagarAutor/{authorId}")]
        public async Task<MessagingHelper<List<Author>>> DeleteAuthor(long authorId)
        {
            return await _service.DeleteAuthor(authorId);
        }
    }
}
