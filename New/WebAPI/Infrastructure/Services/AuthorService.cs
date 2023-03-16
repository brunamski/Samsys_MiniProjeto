using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Infrastructure.DTOs;
using WebAPI.Infrastructure.Entities;
using WebAPI.Infrastructure.Helpers;

namespace WebAPI.Infrastructure.Services
{
    public class AuthorService
    {

        private readonly AppDBContext _appDbContext;
        public AuthorService(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<MessagingHelper<List<Author>>> GetAuthors()
        {
            var response = new MessagingHelper<List<Author>>();
            string errorMessage = "Error occurred while obtaining data";


            var checkAuthors = await _appDbContext.Authors.ToListAsync();
            if (checkAuthors == null)
            {
                response.Success = false;
                response.Message = errorMessage;
                return response;
            }

            response.Obj = checkAuthors;
            response.Success = true;
            return response;
        }

        public async Task<MessagingHelper<List<Author>>> GetAuthor(long Id)
        {
            var response = new MessagingHelper<List<Author>>();
            string notFoundMessage = "Author not found.";
            string foundMessage = "Author found.";

            var author = _appDbContext.Authors.Find(Id);

            if (author == null)
            {
                response.Success = false;
                response.Message = notFoundMessage;
                return response;
            }

            response.Obj = new List<Author> { author };
            response.Success = true;
            response.Message = foundMessage;
            return response;
        }

        public async Task<MessagingHelper<List<AuthorDTO>>> AddAuthor(AuthorDTO objAuthor)
        {
            var response = new MessagingHelper<List<AuthorDTO>>();
            string errorMessage = "Error occurred while adding data";
            string createdMessage = "Author created.";



            if (objAuthor == null)
            {
                response.Success = false;
                response.Message = errorMessage;
                return response;
            }


            Author author = new Author();
            author.name = objAuthor.name;

            _appDbContext.Authors.Add(author);
            await _appDbContext.SaveChangesAsync();

            response.Obj = new List<AuthorDTO> { objAuthor };
            response.Success = true;
            response.Message = createdMessage;
            return response;
        }

        public async Task<MessagingHelper<List<Author>>> UpdateAuthor(long authorId, [FromBody] Author authorToUpdate)
        {
            var response = new MessagingHelper<List<Author>>();
            string errorMessage = "Error occurred while updating data";
            string notFoundMessage = "Author not found.";
            string updatedMessage = "Author updated.";

            var author = await _appDbContext.Authors.FindAsync(authorId);

            if (author == null)
            {
                response.Success = false;
                response.Message = notFoundMessage;
                return response;
            }
            
            if (authorId != authorToUpdate.authorId  || authorToUpdate.name == null)
            {
                response.Success = false;
                response.Message = errorMessage;
                return response;
            }

            

            author.authorId = authorToUpdate.authorId;
            author.name = authorToUpdate.name;

            _appDbContext.Entry(author).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();

            response.Obj = new List<Author> { author };
            response.Success = true;
            response.Message = updatedMessage;
            return response;
        }

        public async Task<MessagingHelper<List<Author>>> DeleteAuthor(long authorId)
        {

            var response = new MessagingHelper<List<Author>>();
            string notFoundMessage = "Author not found.";
            string deletedMessage = "Author deleted.";

            var checkIfAuthorExists = _appDbContext.Authors.Find(authorId);

            if (checkIfAuthorExists != null)
            {
                _appDbContext.Entry(checkIfAuthorExists).State = EntityState.Deleted;
                _appDbContext.SaveChanges();

                response.Obj = new List<Author> { checkIfAuthorExists };
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