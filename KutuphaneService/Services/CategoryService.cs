using System.Resources;
using AutoMapper;
using KutuphaneCore.Entities;
using KutuphaneDataAcces.Dtos.Categories;
using KutuphaneDataAcces.Repository;
using KutuphaneService.Interfaces;
using KutuphaneService.Response;

namespace KutuphaneService.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<IResponse<CategoryDto>> Create(CategoryDto category)
        {
            try
            {
                if (category is null)
                {
                    return GenericResponse<CategoryDto>.Error("Kategori bulunamadı.");
                }

                return GenericResponse<CategoryDto>.Success("Kategori Bulundu", category);
                

                var categoryAsEntity = _mapper.Map<Category>(category);
                _categoryRepository.Create(categoryAsEntity);
            }
            catch (Exception)
            {

                return GenericResponse<CategoryDto>.Error("HATAAAA");
            }
           
        }

        public IResponse<QueryCategoryDto> Delete(int id)
        {
            try
            {
                var categories = _categoryRepository.GetByIdAsync(id).Result;

                if (categories is null)
                {
                    return GenericResponse<QueryCategoryDto>.Error("Kategori bulunamadı.");
                }
                _categoryRepository.Delete(categories);
                return GenericResponse<QueryCategoryDto>.Success("Başarıyla silindi",null);
            }
            catch (Exception ex)
            {

                return GenericResponse<QueryCategoryDto>.Error("HATAAA");
            }
            
        }

        public IResponse<QueryCategoryDto> GetById(int id)
        {
            try
            {
                var categories = _categoryRepository.GetByIdAsync(id).Result;

                var categoryAsDto=_mapper.Map<QueryCategoryDto>(categories);


                if (categoryAsDto is null)
                {
                    return GenericResponse<QueryCategoryDto>.Error("Kategori yok");
                }
                return GenericResponse<QueryCategoryDto>.Success("Kategori başarıyla geldi", categoryAsDto);
            }
            catch (Exception ex)
            {

                return GenericResponse<QueryCategoryDto>.Error("HATAAA!!");
            }
            
        }

        public IResponse<IEnumerable<QueryCategoryDto>> GetByName(string name)
        {
            try
            {
                var categoryName = _categoryRepository.GetAll().Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();

                var categoryNameAsDto = _mapper.Map<IEnumerable<QueryCategoryDto>>(categoryName);

                if (categoryNameAsDto is null || categoryNameAsDto.ToList().Count == 0)
                {
                    return GenericResponse<IEnumerable<QueryCategoryDto>>.Error("Kategori ismi bulunamadı");
                }

                return GenericResponse<IEnumerable<QueryCategoryDto>>.Success("Adıyla çağrılan kategori:", categoryNameAsDto);
            }
            catch (Exception ex)
            {

                return GenericResponse<IEnumerable<QueryCategoryDto>>.Error("HATAA");
            }
            
        }

        public IResponse<IEnumerable<QueryCategoryDto>> ListAll()
        {
            try
            {
                var allCategories = _categoryRepository.GetAll().ToList();

                var allCategoriesAsDto = _mapper.Map<IEnumerable<QueryCategoryDto>>(allCategories);

                if (allCategoriesAsDto is null || allCategoriesAsDto.ToList().Count == 0)
                {
                    return GenericResponse<IEnumerable<QueryCategoryDto>>.Error("Kategoriler Bulunamadı");
                }
                return GenericResponse<IEnumerable<QueryCategoryDto>>.Success("Getirilen Kategoriler", allCategoriesAsDto);
            }
            catch (Exception ex)
            {

                return GenericResponse<IEnumerable<QueryCategoryDto>>.Error("HATAAAA!!");
            }
            
        }

        public IResponse<UpdateCategoryDto> Update(UpdateCategoryDto categoryUpdate)
        {
            try
            {
                var update = _categoryRepository.GetByIdAsync(categoryUpdate.Id).Result;

                if (update is null)
                {
                    return GenericResponse<UpdateCategoryDto>.Error("Kategori bulunamadı.");
                }

                if(!string.IsNullOrEmpty(categoryUpdate.Name))
                {
                    update.Name = categoryUpdate.Name;
                }
                if (!string.IsNullOrEmpty(categoryUpdate.Description))
                {
                    update.Description = categoryUpdate.Description;
                }
                _categoryRepository.Update(update);
                return GenericResponse<UpdateCategoryDto>.Success("Kategori başarıyla güncellendi", categoryUpdate);

            }
            catch (Exception ex)
            {
                return GenericResponse<UpdateCategoryDto>.Error("HATAA: " + ex.Message);
            }
        }

       
    }
}
