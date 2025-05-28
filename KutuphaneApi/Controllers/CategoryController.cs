using KutuphaneDataAcces.Dtos.Categories;
using KutuphaneService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneApi.Controllers
{

    public class CategoryController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _categoryService.ListAll();
            if (!categories.IsSuccess)
            {
                return NotFound(categories.Message);
            }

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _categoryService.GetById(id);
            if (!category.IsSuccess)
            {
                return NotFound(category.Message);
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto category)
        {
            if (category == null)
            {
                return BadRequest("Kategori bilgisi eksik.");
            }
            var result = await _categoryService.Create(category);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

       

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _categoryService.Delete(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result);
        }
        [HttpGet("GetByName/{name}")]
        public IActionResult GetByName(string name)
        {
            var categories = _categoryService.GetByName(name);
            if (!categories.IsSuccess)
            {
                return NotFound(categories.Message);
            }
            return Ok(categories);

        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateCategoryDto categoryUpdate)
        {
            if (categoryUpdate == null)
            {
                return BadRequest("Güncelleme bilgisi eksik.");
            }
            var result = _categoryService.Update(categoryUpdate);
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result);
        }
    }
}