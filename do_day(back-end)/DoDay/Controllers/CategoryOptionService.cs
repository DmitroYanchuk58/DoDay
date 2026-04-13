using API_Layer.DTO;
using Business_Logic_Layer.DTO;
using Business_Logic_Layer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryOption : ControllerBase
    {
        public ICategoryOptionService _service;

        public CategoryOption(ICategoryOptionService service)
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
                Id = categoryOption.Id ?? Guid.NewGuid(),
                Key = categoryOption.Key ?? 5,
                Value = categoryOption.Value,
                CategoryId = categoryOption.IdCategory ?? Guid.Empty
            };
            await _service.CreateCategoryOption(categoryOptionDTO);
            return Ok();
        }

        [HttpDelete("DeleteCategoryOption")]
        public async Task<IActionResult> DeleteCategoryOption(Guid id)
        {
            await _service.DeleteCategoryOption(id);
            return Ok();
        }

        [HttpPut("UpdateCategoryOption")]
        public async Task<IActionResult> UpdateCategoryOption(CategoryOptionForRequest categoryOption)
        {
            var categoryOptionDTO = new CategoryOptionDTO
            {
                Id = categoryOption.Id ?? Guid.Empty,
                Key = categoryOption.Key ?? 5,
                Value = categoryOption.Value,
                CategoryId = categoryOption.IdCategory ?? Guid.Empty
            };
            await _service.UpdateCategoryOption(categoryOptionDTO);
            return Ok();
        }
    }
}
