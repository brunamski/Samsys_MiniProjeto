using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {

        private readonly BookDBContext _livroDbContext;

        public LivroController(BookDBContext livroDbContext)
        {
            _livroDbContext = livroDbContext;
        }



        [HttpGet]
        [Route("livros")]
        public async Task<ActionResult<Livro>> GetLivros()
        {
            var checkLivros = await _livroDbContext.Livros.ToListAsync();
            if (checkLivros == null)
            {
                return NoContent();
            }
            return Ok(checkLivros);
        }

        [HttpGet]
        [Route("livros/{isbn}")]
        public async Task<ActionResult<Livro>> GetLivro(string isbn)
        {
            var livro = _livroDbContext.Livros.Find(isbn);

            if (livro == null)
            {
                return NotFound();
            }

            return Ok(livro);
        }


        [HttpPost]
        [Route("criarLivro")]
        public async Task<ActionResult<Livro>> AddLivro(Livro objLivro)
        {
            if (objLivro.isbn.Length != 13 || objLivro.preco < 0 || objLivro == null)
            {
                return BadRequest();
            }

            var checkIfLivroExists = _livroDbContext.Livros.Find(objLivro.isbn);
            if (checkIfLivroExists != null && checkIfLivroExists.isbn == objLivro.isbn)
            {
                return Conflict();
            }

            _livroDbContext.Livros.Add(objLivro);
            await _livroDbContext.SaveChangesAsync();
            return StatusCode(201);
        }

        [HttpPatch]
        [Route("atualizarLivro/{isbn}")]
        public async Task<ActionResult<Livro>> UpdateLivro(string isbn, [FromBody] Livro livroToUpdate)
        {
            if (isbn != livroToUpdate.isbn || livroToUpdate.isbn.Length != 13 || livroToUpdate.preco < 0 || livroToUpdate == null)
            {
                return BadRequest();
            }

            var livro = await _livroDbContext.Livros.FindAsync(isbn);

            if (livro == null)
            {
                return NotFound();
            }

            livro.name = livroToUpdate.name;
            livro.author = livroToUpdate.author;
            livro.preco = livroToUpdate.preco;

            _livroDbContext.Entry(livro).State = EntityState.Modified;
            await _livroDbContext.SaveChangesAsync();
            return Ok(livro);
        }




        [HttpDelete]
        [Route("apagarLivro/{isbn}")]
        public async Task<ActionResult<Livro>> DeleteLivro(string isbn)
        {

            var checkIfLivroExists = _livroDbContext.Livros.Find(isbn);

            if (checkIfLivroExists != null)
            {
                _livroDbContext.Entry(checkIfLivroExists).State = EntityState.Deleted;
                _livroDbContext.SaveChanges();
                return StatusCode(204,"Deleted");
            }
            return NotFound();
        }
    }
}
