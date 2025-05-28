using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using KutuphaneCore.Entities;
using KutuphaneDataAcces.Dtos.Authors;
using KutuphaneDataAcces.Repository;
using KutuphaneService.Interfaces;
using KutuphaneService.Response;

namespace KutuphaneService.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IGenericRepository<Author> _authorRepository;
        private readonly IMapper _mapper;
        public AuthorService(IGenericRepository<Author> authorRepository,IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public Task<IResponse<Author>> Create(AuthorDto author)
        {
            try
            {
                if (author is null)
                {
                    return Task.FromResult<IResponse<Author>>(GenericResponse<Author>.Error("Yazar Bilgileri Boş Olamaz."));
                }


                var authorDto = _mapper.Map<Author>(author);
                authorDto.RecordTime = DateTime.Now;


                //var authorDtos= new Author()
                //{
                //    Name = author.Name,
                //    Surname = author.Surname,
                //    PlaceofBirth = author.PlaceofBirth,
                //    YearOfBirth = author.YearOfBirth,
                //    RecordTime = DateTime.Now
                //};

                _authorRepository.Create(authorDto);
                return Task.FromResult<IResponse<Author>>(GenericResponse<Author>.Success("Yazar başarıyla oluşturuldu."));

            }catch(Exception ex)
            {
                return Task.FromResult<IResponse<Author>>(GenericResponse<Author>.Error("HATAAA!!"));
            }
            
        }

        public IResponse<Author> Delete(int id)
        {
            try
            {
                var author = _authorRepository.GetByIdAsync(id).Result;

                if (author is null)
                {
                    return GenericResponse<Author>.Error("Yazar Bulunamadı.");
                }

                _authorRepository.Delete(author);
                return GenericResponse<Author>.Success("Yazar başarıyla silindi.", author);
            }catch(Exception ex)
            {

                return GenericResponse<Author>.Error("HATAAA");
            }
            
        }

        public IResponse<QueryAuthorDto> GetById(int id)
        {
            try
            {
                var author = _authorRepository.GetByIdAsync(id).Result;

                var authorDto = _mapper.Map<QueryAuthorDto>(author);

                if (authorDto is null)
                {
                    return GenericResponse<QueryAuthorDto>.Error("Yazar Bulunamadı");
                }
                return GenericResponse<QueryAuthorDto>.Success("Author", authorDto);

            }catch (Exception ex)
            { return GenericResponse<QueryAuthorDto>.Error("HATAAAA!!");
            
            }
      
        }

        public IResponse<IEnumerable<QueryAuthorDto>> GetByName(string name)
        {

            try
            {
                var authorList = _authorRepository.GetAll().Where(x => x.Name == name).ToList();

                var authorDtos= _mapper.Map<IEnumerable<QueryAuthorDto>>(authorList);

                if (authorDtos is null || authorDtos.ToList().Count == 0)
                {
                    return GenericResponse<IEnumerable<QueryAuthorDto>>.Error("Yazar Bulunamadı");
                }

                return GenericResponse<IEnumerable<QueryAuthorDto>>.Success("Yazar:", authorDtos);
            }catch(Exception ex)
            {
                return GenericResponse<IEnumerable<QueryAuthorDto>>.Error("HATAAAA");
            }
            
        }

        public IResponse<IEnumerable<QueryAuthorDto>> ListAll()
        {
            try
            {
                var allAuthors = _authorRepository.GetAll().ToList();

                var authorDtos = _mapper.Map<IEnumerable<QueryAuthorDto>>(allAuthors);

                if (authorDtos is null || authorDtos.ToList().Count==0)
                {
                    return GenericResponse<IEnumerable<QueryAuthorDto>>.Error("Yazarlar Bulunamadı");
                }

                return GenericResponse<IEnumerable<QueryAuthorDto>>.Success("Yazarlar:", authorDtos);
            }catch(Exception ex)
            {
                return GenericResponse<IEnumerable<QueryAuthorDto>>.Error("HATAA");
            }
            
        }

        public IResponse<UpdateAuthorDto> Update(UpdateAuthorDto authorUpdate)
        {
            try
            {
                var upAuthor = _authorRepository.GetByIdAsync(authorUpdate.Id).Result;

                if (upAuthor is null)
                {
                    return GenericResponse<UpdateAuthorDto>.Error("Yazar Bulunamadı.");
                }

                if(!string.IsNullOrEmpty(authorUpdate.Name))
                {
                    upAuthor.Name = authorUpdate.Name;
                }
                if (!string.IsNullOrEmpty(authorUpdate.Surname))
                {
                    upAuthor.Surname = authorUpdate.Surname;
                }
                if (!string.IsNullOrEmpty(authorUpdate.PlaceofBirth))
                {
                    upAuthor.PlaceofBirth = authorUpdate.PlaceofBirth;
                }

                if (authorUpdate.YearOfBirth != 0)
                {
                    upAuthor.YearOfBirth = authorUpdate.YearOfBirth;
                }
                _authorRepository.Update(upAuthor);
                return GenericResponse<UpdateAuthorDto>.Success("Yazar başarıyla güncellendi.", authorUpdate);
            }
            catch (Exception ex)
            {

                return GenericResponse<UpdateAuthorDto>.Error("HATAA: " + ex.Message);
            }
        }

      
    }
}
