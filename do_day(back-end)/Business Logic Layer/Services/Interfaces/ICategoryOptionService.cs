using Business_Logic_Layer.DTO;

namespace Business_Logic_Layer.Services.Interfaces
{
    public interface ICategoryOptionService
    {
        public Task CreateCategoryOption(CategoryOptionDTO categoryOptionDTO);

        public Task DeleteCategoryOption(Guid id);

        public Task<CategoryOptionDTO> GetCategoryOption(Guid id);

        public Task UpdateCategoryOption(CategoryOptionDTO categoryOptionDTO);
    }
}
