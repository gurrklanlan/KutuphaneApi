using KutuphaneDataAcces.Dtos.Books;
using KutuphaneService.Interfaces;
using KutuphaneService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneApi.Controllers
{

    public class BookController : CustomBaseController
    {
        private readonly IBooksService _bookService;
        public BookController(IBooksService bookService)
        {
            _bookService = bookService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = _bookService.ListAll();
            if (!books.IsSuccess)
            {
                return NotFound(books.Message);
            }
            return Ok(books);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = _bookService.GetById(id);
            if (!book.IsSuccess)
            {
                return NotFound(book.Message);
            }
            return Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookDto book)
        {
            if (book == null)
            {
                return BadRequest("Kitap verisi boş.");
            }
            var response = await _bookService.Create(book);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var response = _bookService.Delete(id);
            if (!response.IsSuccess)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }
        [HttpGet("GetByName/{name}")]
        public IActionResult GetByName(string name)
        {
            var response = _bookService.GetByName(name);
            if (!response.IsSuccess)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpGet("GetBooksByCategoryId/{categoryId:int}")]
        public IActionResult GetByCategoryId(int categoryId)
        {
            var response = _bookService.GetBooksByCategoryId(categoryId);
            if (!response.IsSuccess)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpGet("GetBooksByAuthorId/{authorId:int}")]
        public IActionResult GetByAuthorId(int authorId)
        {
            var response = _bookService.GetBooksByAuthorId(authorId);
            if (!response.IsSuccess)
            {
                return NotFound(response.Message);
            }
            return Ok(response);

        }
        [HttpPut]
        public IActionResult Update([FromBody] UpdateBookDto updatedBook)
        {
            if(updatedBook is null)
            {
                return BadRequest("Güncellenecek kitap verisi boş.");
            }

            var result = _bookService.Update(updatedBook);
            if (!result.Result.IsSuccess)
            {
                return NotFound($"Güncellenecek kitap bulunamadı: {updatedBook.Id}");
            }

            return Ok(result);
        }
    }
}
