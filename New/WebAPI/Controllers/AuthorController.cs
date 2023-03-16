using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;
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
        [Route("autores/{Id}")]
        public async Task<MessagingHelper<List<Author>>> GetAuthorByName(long Id)
        {
            return await _service.GetAuthor(Id);
        }


        [HttpPost]
        [Route("criarAutor")]

        public async Task<MessagingHelper<List<Author>>> AddAuthor(Author objAuthor)
        {
            return await _service.AddAuthor(objAuthor);
        }

        [HttpPatch]
        [Route("atualizarAutor/{Id}")]
        public async Task<MessagingHelper<List<Author>>> UpdateAuthor(long Id, [FromBody] Author authorToUpdate)
        {
            return await _service.UpdateAuthor(Id, authorToUpdate);
        }




        [HttpDelete]
        [Route("apagarAutor/{Id}")]
        public async Task<MessagingHelper<List<Author>>> DeleteAuthor(long Id)
        {
            return await _service.DeleteAuthor(Id);
        }
    }
}
