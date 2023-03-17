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
using WebAPI.Mappers;

namespace WebAPI.Infrastructure.Services
{
    public class AuthorService
    {

        private readonly AppDBContext _appDbContext;
        private readonly IMapper _mapper;
        public AuthorService(AppDBContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }


        public async Task<MessagingHelper<List<GetAuthorsInfoDTO>>> GetAuthors()
        {
            var response = new MessagingHelper<List<GetAuthorsInfoDTO>>();
            string errorMessage = "Error occurred while obtaining data";


            var checkAuthors = await _appDbContext.Authors.ToListAsync();
            var getAuthorsInfoDTO = _mapper.Map<List<GetAuthorsInfoDTO>>(checkAuthors);

            if (checkAuthors == null)
            {
                response.Success = false;
                response.Message = errorMessage;
                return response;
            }

            response.Obj = getAuthorsInfoDTO;
            response.Success = true;
            return response;
        }

        public async Task<MessagingHelper<List<GetAuthorsInfoDTO>>> GetAuthor(long authorId)
        {
            var response = new MessagingHelper<List<GetAuthorsInfoDTO>>();
            string notFoundMessage = "Author not found.";
            string foundMessage = "Author found.";

            var author = _appDbContext.Authors.Find(authorId);
            var getAuthorsInfoDTO = _mapper.Map<GetAuthorsInfoDTO>(author);

            if (author == null)
            {
                response.Success = false;
                response.Message = notFoundMessage;
                return response;
            }

            response.Obj = new List<GetAuthorsInfoDTO> { getAuthorsInfoDTO };
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

            // tentar usar o mapper

            // Map AuthorDTO to Author entity
            var author = _mapper.Map<Author>(objAuthor);

            _appDbContext.Authors.Add(author);
            await _appDbContext.SaveChangesAsync();

            // Map Author entity back to AuthorDTO
            //var authorDTO = _mapper.Map<AuthorDTO>(author);

            response.Obj = new List<AuthorDTO> { objAuthor };
            response.Success = true;
            response.Message = createdMessage;
            return response;
        }

        public async Task<MessagingHelper<List<GetAuthorsInfoDTO>>> UpdateAuthor(long authorId, [FromBody] GetAuthorsInfoDTO authorToUpdate)
        {
            var response = new MessagingHelper<List<GetAuthorsInfoDTO>>();
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

            var getAuthorsInfoDTO = _mapper.Map<GetAuthorsInfoDTO>(author);

            response.Obj = new List<GetAuthorsInfoDTO> { getAuthorsInfoDTO } ;
            response.Success = true;
            response.Message = updatedMessage;
            return response;
        }

        public async Task<MessagingHelper<List<GetAuthorsInfoDTO>>> DeleteAuthor(long authorId)
        {

            var response = new MessagingHelper<List<GetAuthorsInfoDTO>>();
            string notFoundMessage = "Author not found.";
            string deletedMessage = "Author deleted.";

            var checkIfAuthorExists = _appDbContext.Authors.Find(authorId);

            var getAuthorsInfoDTO = _mapper.Map<GetAuthorsInfoDTO>(checkIfAuthorExists);

            if (checkIfAuthorExists != null)
            {
                _appDbContext.Entry(checkIfAuthorExists).State = EntityState.Deleted;
                _appDbContext.SaveChanges();

                response.Obj = new List<GetAuthorsInfoDTO> { getAuthorsInfoDTO };
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