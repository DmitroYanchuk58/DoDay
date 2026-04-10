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
    }
}
