using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [Route("api/Livro")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly BookDBContext _bookDbContext;

        public BookController(BookDBContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }



        [HttpGet]
        [Route("livros")]
        public async Task<ActionResult<Book>> GetLivros()
        {
            var checkLivros = await _bookDbContext.Books.ToListAsync();
            if (checkLivros == null)
            {
                return NoContent();
            }
            return Ok(checkLivros);
        }

        [HttpGet]
        [Route("livros/{isbn}")]
        public async Task<ActionResult<Book>> GetLivro(string isbn)
        {
            var livro = _bookDbContext.Books.Find(isbn);

            if (livro == null)
            {
                return NotFound();
            }

            return Ok(livro);
        }


        [HttpPost]
        [Route("criarLivro")]
        public async Task<ActionResult<Book>> AddLivro(Book objLivro)
        {
            if (objLivro.isbn.Length != 13 || objLivro.price < 0 || objLivro == null)
            {
                return BadRequest();
            }

            var checkIfLivroExists = _bookDbContext.Books.Find(objLivro.isbn);
            if (checkIfLivroExists != null && checkIfLivroExists.isbn == objLivro.isbn)
            {
                return Conflict();
            }

            _bookDbContext.Books.Add(objLivro);
            await _bookDbContext.SaveChangesAsync();
            return StatusCode(201);
        }

        [HttpPatch]
        [Route("atualizarLivro/{isbn}")]
        public async Task<ActionResult<Book>> UpdateLivro(string isbn, [FromBody] Book livroToUpdate)
        {
            if (isbn != livroToUpdate.isbn || livroToUpdate.isbn.Length != 13 || livroToUpdate.price < 0 || livroToUpdate == null)
            {
                return BadRequest();
            }

            var livro = await _bookDbContext.Books.FindAsync(isbn);

            if (livro == null)
            {
                return NotFound();
            }

            livro.name = livroToUpdate.name;
            livro.author = livroToUpdate.author;
            livro.price = livroToUpdate.price;

            _bookDbContext.Entry(livro).State = EntityState.Modified;
            await _bookDbContext.SaveChangesAsync();
            return Ok(livro);
        }




        [HttpDelete]
        [Route("apagarLivro/{isbn}")]
        public async Task<ActionResult<Book>> DeleteLivro(string isbn)
        {

            var checkIfLivroExists = _bookDbContext.Books.Find(isbn);

            if (checkIfLivroExists != null)
            {
                _bookDbContext.Entry(checkIfLivroExists).State = EntityState.Deleted;
                _bookDbContext.SaveChanges();
                return StatusCode(204,"Deleted");
            }
            return NotFound();
        }
    }
}
