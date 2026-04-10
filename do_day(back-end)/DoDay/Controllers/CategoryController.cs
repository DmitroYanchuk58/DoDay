using API_Layer.DTO;
using Business_Logic_Layer.DTO;
using Business_Logic_Layer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategory(Guid idCategory)
        {
            var category = await _service.GetCategory(idCategory);
            return Ok(category);
        }

        [HttpGet("GetCategoriesByUser")]
        public async Task<IActionResult> GetCategoriesByUser(Guid idUser)
        {
            var categories = await _service.GetAllUserCategories(idUser);
            return Ok(categories);
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CategoryForRequest categoryRequest)
        {
            var categoryDto = new CategoryDTO
            {
                Id = categoryRequest.Id,
                Name = categoryRequest.Name,
                IdUser = categoryRequest.IdUser
            };
            await _service.CreateCategory(categoryDto);
            return Ok();
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryForRequest categoryRequest)
        {
            var categoryDto = new CategoryDTO
            {
                Id = categoryRequest.Id,
                Name = categoryRequest.Name,
                IdUser = categoryRequest.IdUser
            };
            await _service.UpdateCategory(categoryDto);
            return Ok();
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(Guid idCategory)
        {
            await _service.DeleteCategory(idCategory);
            return Ok();
        }
    }
}
