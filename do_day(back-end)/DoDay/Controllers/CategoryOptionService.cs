using API_Layer.DTO;
using Business_Logic_Layer.DTO;
using Business_Logic_Layer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryOptionService : ControllerBase
    {
        public ICategoryOptionService _service;

        public CategoryOptionService(ICategoryOptionService service)
        {
            _service = service;
        }

        [HttpGet("GetCategoryOption")]
        public async Task<IActionResult> GetCategoryOption(Guid id)
        {
            var categoryOption = await _service.GetCategoryOption(id);
            return Ok(categoryOption);
        }

        [HttpPost("CreateCategoryOption")]
        public async Task<IActionResult> CreateCategoryOption(CategoryOptionForRequest categoryOption)
        {
            var categoryOptionDTO = new CategoryOptionDTO
            {
                Id = categoryOption.Id,
                Key = categoryOption.Key,
                Value = categoryOption.Value,
                CategoryId = categoryOption.IdCategory
            };
            await _service.CreateCategoryOption(categoryOptionDTO);
            return Ok();
        }
    }
}
