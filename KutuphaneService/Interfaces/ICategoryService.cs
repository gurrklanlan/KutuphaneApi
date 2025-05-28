using KutuphaneCore.Entities;
using KutuphaneDataAcces.Dtos.Categories;

namespace KutuphaneService.Interfaces
{
    public interface ICategoryService
    {
        IResponse<IEnumerable<QueryCategoryDto>> ListAll();
        IResponse<QueryCategoryDto> GetById(int id);
        Task<IResponse<CategoryDto>> Create(CategoryDto category);
        IResponse<UpdateCategoryDto> Update(UpdateCategoryDto categoryUpdate);
        IResponse<QueryCategoryDto> Delete(int id);
        IResponse<IEnumerable<QueryCategoryDto>> GetByName(string name);
    }
}
