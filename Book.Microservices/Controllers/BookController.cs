using Entities;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Book.Microservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository bookRepository;
        public BookController(IBookRepository _bookRepository)
        {
            bookRepository = _bookRepository;
        }


        [HttpGet("Get")]
        public async Task<ApiResponse> Get([FromBody] Entities.Book book)
        {
            var data = await this.bookRepository.Get(book);

            if (data != null)
            {
                return ApiResponse.Success(JsonSerializer.Serialize(data), "Books data gets successfully.");
            }

            return ApiResponse.Fail("Error Occured while getting the data");
        }

        [HttpPost("Add")]
        public async Task<ApiResponse> Add(Entities.Book book)
        {

            var result = await this.bookRepository.Add(book);

            if (result != 0)
            {
                return ApiResponse.Success(null, "Book added successfully.");
            }

            return ApiResponse.Fail("Error Occured while adding the book");
        }

        [HttpPost("Update")]
        public async Task<ApiResponse> Update(Entities.Book book)
        {

            var result = await this.bookRepository.Update(book);

            if (result != 0)
            {
                return ApiResponse.Success(null, "Book updated successfully.");
            }

            return ApiResponse.Fail("Error Occured while updating book");
        }

        [HttpDelete("Delete")]
        public async Task<ApiResponse> Delete(int id)
        {

            await this.bookRepository.Delete(id);

            try
            {
                return ApiResponse.Success(null, "Book deleted successfully.");

            }
            catch (Exception e)
            {
                return ApiResponse.Fail("Error Occured while deleting book");
            }
        }
    }
}
