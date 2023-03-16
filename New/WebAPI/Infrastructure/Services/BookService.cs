using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Infrastructure.Helpers;

namespace WebAPI.Infrastructure.Services
{
    public class BookService
    {

        private readonly AppDBContext _appDbContext;
        public BookService(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<MessagingHelper<List<Book>>> GetLivros()
        {
            var response = new MessagingHelper<List<Book>>();
            string errorMessage = "Error occurred while obtaining data";


            var checkLivros = await _appDbContext.Books.ToListAsync();
            if (checkLivros == null)
            {
                response.Success = false;
                response.Message = errorMessage;
                return response;
            }

            response.Obj = checkLivros;
            response.Success = true;
            return response;
        }

        public async Task<MessagingHelper<List<Book>>> GetLivro(string isbn)
        {
            var response = new MessagingHelper<List<Book>>();
            string notFoundMessage = "Book not found.";
            string foundMessage = "Book found.";

            var livro = _appDbContext.Books.Find(isbn);

            if (livro == null)
            {
                response.Success = false;
                response.Message = notFoundMessage;
                return response;
            }

            response.Obj = new List<Book> { livro };
            response.Success = true;
            response.Message = foundMessage;
            return response;
        }

        public async Task<MessagingHelper<List<Book>>> AddLivro(Book objLivro)
        {
            var response = new MessagingHelper<List<Book>>();
            string errorMessage = "Error occurred while adding data";
            string isbnAlreadyExistsMessage = "Book with the provided ISBN already exists.";
            string createdMessage = "Book created.";



            if (objLivro.isbn.Length != 13 || objLivro.price < 0 || objLivro == null)
            {
                response.Success = false;
                response.Message = errorMessage;
                return response;
            }

            var checkIfLivroExists = _appDbContext.Books.Find(objLivro.isbn);
            if (checkIfLivroExists != null && checkIfLivroExists.isbn == objLivro.isbn)
            {
                response.Success = false;
                response.Message = isbnAlreadyExistsMessage;
                return response;
            }

            _appDbContext.Books.Add(objLivro);
            await _appDbContext.SaveChangesAsync();

            response.Obj = new List<Book> { objLivro };
            response.Success = true;
            response.Message = createdMessage;
            return response;
        }

        public async Task<MessagingHelper<List<Book>>> UpdateLivro(string isbn, [FromBody] Book livroToUpdate)
        {
            var response = new MessagingHelper<List<Book>>();
            string errorMessage = "Error occurred while updating data";
            string notFoundMessage = "Book not found.";
            string updatedMessage = "Book updated."; 
            
            if (isbn != livroToUpdate.isbn || livroToUpdate.isbn.Length != 13 || livroToUpdate.price < 0 || livroToUpdate == null)
            {
                response.Success = false;
                response.Message = errorMessage;
                return response;
            }

            var livro = await _appDbContext.Books.FindAsync(isbn);

            if (livro == null)
            {
                response.Success = false;
                response.Message = notFoundMessage;
                return response;
            }

            livro.name = livroToUpdate.name;
            livro.author = livroToUpdate.author;
            livro.price = livroToUpdate.price;

            _appDbContext.Entry(livro).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();

            response.Obj = new List<Book> { livro };
            response.Success = true;
            response.Message = updatedMessage;
            return response;
        }

        public async Task<MessagingHelper<List<Book>>> DeleteLivro(string isbn)
        {

            var response = new MessagingHelper<List<Book>>();
            string errorMessage = "Error occurred while deleting data";
            string notFoundMessage = "Book not found.";
            string deletedMessage = "Book deleted."; 
            
            var checkIfLivroExists = _appDbContext.Books.Find(isbn);

            if (checkIfLivroExists != null)
            {
                _appDbContext.Entry(checkIfLivroExists).State = EntityState.Deleted;
                _appDbContext.SaveChanges();

                response.Obj = new List<Book> { checkIfLivroExists };
                response.Success = true;
                response.Message = deletedMessage;
                return response;
                
            }

            response.Success = false;
            response.Message = notFoundMessage;
            return response;

        }

    }


}
