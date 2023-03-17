using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Infrastructure.DTOs;
using WebAPI.Infrastructure.Entities;
using WebAPI.Infrastructure.Helpers;

namespace WebAPI.Infrastructure.Services
{
    public class BookService
    {

        private readonly AppDBContext _appDbContext;
        private readonly IMapper _mapper;
        public BookService(AppDBContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;   
        }


        public async Task<MessagingHelper<List<BookDTO>>> GetLivros()
        {
            var response = new MessagingHelper<List<BookDTO>>();
            string errorMessage = "Error occurred while obtaining data";

            var checkLivros = await _appDbContext.Books.Include(x => x.author).ToListAsync();

            if (checkLivros == null)
            {
                response.Success = false;
                response.Message = errorMessage;
                return response;
            }

            var livrosDTO = _mapper.Map<List<BookDTO>>(checkLivros);

            response.Obj = livrosDTO;
            response.Success = true;
            return response;
        }




        public async Task<MessagingHelper<List<BookDTO>>> GetLivro(string isbn)
        {
            var response = new MessagingHelper<List<BookDTO>>();
            string notFoundMessage = "Book not found.";
            string foundMessage = "Book found.";

            var livro = _appDbContext.Books.Include(x => x.author).SingleOrDefault(x => x.isbn == isbn);

            var bookDetailsDTO = _mapper.Map<BookDTO>(livro);

            if (livro == null)
            {
                response.Success = false;
                response.Message = notFoundMessage;
                return response;
            }

            response.Obj = new List<BookDTO> { bookDetailsDTO };
            response.Success = true;
            response.Message = foundMessage;
            return response;
        }


        public async Task<MessagingHelper<List<AddBookDTO>>> AddLivro(AddBookDTO objLivro)
        {
            var response = new MessagingHelper<List<AddBookDTO>>();
            string errorMessage = "Error occurred while adding data";
            string isbnAlreadyExistsMessage = "Book with the provided ISBN already exists.";
            string authorNotExists = "Author provided does not exist.";
            string createdMessage = "Book created.";

            //validations
            if (objLivro.isbn.Length != 13 || objLivro.price < 0 || objLivro == null)
            {
                response.Success = false;
                response.Message = errorMessage;
                return response;
            }

            // Check if book exists
            var checkIfLivroExists = _appDbContext.Books.Find(objLivro.isbn);
            if (checkIfLivroExists != null && checkIfLivroExists.isbn == objLivro.isbn)
            {
                response.Success = false;
                response.Message = isbnAlreadyExistsMessage;
                return response;
            }

            // Check if author exists
            var checkIfAuthorExists = _appDbContext.Authors.Find(objLivro.authorId);
            if (checkIfAuthorExists == null)
            {
                response.Success = false;
                response.Message = authorNotExists;
                return response;
            }

            // Map AddBookDTO to Book entity
            var book = _mapper.Map<Book>(objLivro);

            _appDbContext.Books.Add(book);
            await _appDbContext.SaveChangesAsync();


            response.Obj = new List<AddBookDTO> { objLivro };
            response.Success = true;
            response.Message = createdMessage;
            return response;
        }


        public async Task<MessagingHelper<List<AddBookDTO>>> UpdateLivro(string isbn, [FromBody] AddBookDTO livroToUpdate)
        {
            var response = new MessagingHelper<List<AddBookDTO>>();
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
            livro.authorId = livroToUpdate.authorId;
            livro.price = livroToUpdate.price;

            _appDbContext.Entry(livro).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();

            var addLivroDTO = _mapper.Map<AddBookDTO>(livro);

            response.Obj = new List<AddBookDTO> { addLivroDTO };
            response.Success = true;
            response.Message = updatedMessage;
            return response;
        }

        public async Task<MessagingHelper<List<AddBookDTO>>> DeleteLivro(string isbn)
        {

            var response = new MessagingHelper<List<AddBookDTO>>();
            string errorMessage = "Error occurred while deleting data";
            string notFoundMessage = "Book not found.";
            string deletedMessage = "Book deleted."; 
            
            var checkIfLivroExists = _appDbContext.Books.Find(isbn);

            var addBookDTO = _mapper.Map<AddBookDTO>(checkIfLivroExists);

            if (checkIfLivroExists != null)
            {
                _appDbContext.Entry(checkIfLivroExists).State = EntityState.Deleted;
                _appDbContext.SaveChanges();

                response.Obj = new List<AddBookDTO> { addBookDTO };
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
