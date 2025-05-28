using AutoMapper;
using KutuphaneCore.Entities;
using KutuphaneDataAcces.Dtos.Authors;
using KutuphaneDataAcces.Dtos.Books;
using KutuphaneDataAcces.Dtos.Categories;

namespace KutuphaneService.Mapper
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Author, QueryAuthorDto>().ReverseMap();

            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, QueryBookDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, QueryCategoryDto>().ReverseMap();
        }
    }

}
