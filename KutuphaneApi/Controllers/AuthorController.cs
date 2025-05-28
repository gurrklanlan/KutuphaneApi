using KutuphaneCore.Entities;
using KutuphaneDataAcces.Dtos.Authors;
using KutuphaneService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneApi.Controllers
{

    public class AuthorController : CustomBaseController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [AllowAnonymous]
        [HttpGet]

        public IActionResult ListAll()
        {
            var response = _authorService.ListAll();
            if (!response.IsSuccess)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var response = _authorService.GetById(id);
            if (!response.IsSuccess)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorDto author)
        {
            if (author == null)
            {
                return BadRequest("Author data is null.");
            }
            var response = await _authorService.Create(author);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
      

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var response = _authorService.Delete(id);
            if (!response.IsSuccess)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }


        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Author name cannot be null or empty.");
            }
            var response = _authorService.GetByName(name);
            if (!response.IsSuccess)
            {
                return NotFound(response.Message);
            }
            return Ok(response);

        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateAuthorDto authorUpdate)
        {
            if (authorUpdate == null)
            {
                return BadRequest("Author update data is null.");
            }
            var response = _authorService.Update(authorUpdate);
            if (!response.IsSuccess)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }
    }
}
