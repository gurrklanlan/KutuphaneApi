using System.Formats.Tar;
using AutoMapper;
using KutuphaneCore.Entities;
using KutuphaneDataAcces.Dtos.Books;
using KutuphaneDataAcces.Repository;
using KutuphaneService.Interfaces;
using KutuphaneService.Response;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace KutuphaneService.Services
{
    public class BookService : IBooksService
    {
        protected readonly IGenericRepository<Book> _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;
        public BookService(IGenericRepository<Book> bookRepository, IMapper mapper, ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<IResponse<BookDto>> Create(BookDto book)
        {
           try
            {
                if(book is null)
                {
                    return Task.FromResult<IResponse<BookDto>>(GenericResponse<BookDto>.Error("Oluşturulamadı"));
                }

                var bookAsDto = _mapper.Map<Book>(book);

                _bookRepository.Create(bookAsDto);

                _logger.LogInformation("Kitap başarıyla oluşturuldu: {Title}", book.Title);

                return Task.FromResult<IResponse<BookDto>>(GenericResponse<BookDto>.Success("Başarıyla oluşturuldu"));
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Kitap oluşturulurken hata oluştu: {Message}", ex.Message, book.Title);
                return Task.FromResult<IResponse<BookDto>>(GenericResponse<BookDto>.Error("HATAAA"));
            }
        }

        public IResponse<Book> Delete(int id)
        {
            try
            {
                var books = _bookRepository.GetByIdAsync(id).Result;

                if (books is null)
                {
                    return GenericResponse<Book>.Error("Kitapl bulunamadı");
                }

                return GenericResponse<Book>.Success("Kitap başarıyla silindi", books);
            }catch(Exception ex)
            {
                return GenericResponse<Book>.Error("HATAAA");
            }
            

        }

        public IResponse<QueryBookDto> GetById(int id)
        {
            try
            {
                var books = _bookRepository.GetByIdAsync(id).Result;

                var booksAsDto = _mapper.Map<QueryBookDto>(books);

                if (booksAsDto is null)
                {
                    return GenericResponse<QueryBookDto>.Error("Kitap bulunamadı");
                }

                return GenericResponse<QueryBookDto>.Success("Kitap başarıyla getirildi", booksAsDto);
            }
            catch (Exception ex)
            {

                return GenericResponse<QueryBookDto>.Error("HATAA");
            }
            
        }

        public IResponse<IEnumerable<QueryBookDto>> GetByName(string name)
        {
            try
            {
                var booksName = _bookRepository.GetAll().Where(x => x.Title.ToLower().Contains(name.ToLower())).ToList();

                var booksNameAsDto = _mapper.Map<IEnumerable<QueryBookDto>>(booksName);

                if (booksNameAsDto is null || booksNameAsDto.ToList().Count==0)
                {
                    return GenericResponse<IEnumerable<QueryBookDto>>.Error("Kitap yok");
                }

                return GenericResponse<IEnumerable<QueryBookDto>>.Success("Kitap ismiyle getirildi", booksNameAsDto);
            }
            catch (Exception)
            {

                return GenericResponse<IEnumerable<QueryBookDto>>.Error("HATAAA");
            }
            
        }

        public IResponse<IEnumerable<QueryBookDto>> ListAll()
        {
            try
            {
                var allBooks = _bookRepository.GetAll().ToList();

                var allBooksAsDto = _mapper.Map<IEnumerable<QueryBookDto>>(allBooks);
                
                if (allBooksAsDto is null || allBooksAsDto.ToList().Count==0)
                {
                    return GenericResponse<IEnumerable<QueryBookDto>>.Error("Kitaplae yok");
                }

                return GenericResponse<IEnumerable<QueryBookDto>>.Success("Geitirlen Kitaplar", allBooksAsDto);
            }
            catch (Exception)
            {

                return GenericResponse<IEnumerable<QueryBookDto>>.Error("HATAA!");
            }
            
        }

        public IResponse<IEnumerable<QueryBookDto>> GetBooksByCategoryId(int categoryId)
        {
            try
            {
                var booksWithCategory = _bookRepository.GetAll().Where(x => x.CategoryId == categoryId).ToList();

                var booksWithCategoryAsDto = _mapper.Map<IEnumerable<QueryBookDto>>(booksWithCategory); 

                if(booksWithCategoryAsDto is null || booksWithCategoryAsDto.ToList().Count == 0)
                {
                    return GenericResponse<IEnumerable<QueryBookDto>>.Error("Kitap yok");
                }
                return GenericResponse<IEnumerable<QueryBookDto>>.Success("Kitaplar kategoriye göre getirildi", booksWithCategoryAsDto);
            }
            catch ( Exception ex)
            {

                return GenericResponse<IEnumerable<QueryBookDto>>.Error("HATAA: " + ex.Message);
            }
        }

        public IResponse<IEnumerable<QueryBookDto>> GetBooksByAuthorId(int authorId)
        {
            try
            {
                var bookWithAuthor = _bookRepository.GetAll().Where(x => x.AuthorId == authorId).ToList();


                var bookWithAuthorAsDto = _mapper.Map<IEnumerable<QueryBookDto>>(bookWithAuthor);

                if(bookWithAuthorAsDto is null || bookWithAuthorAsDto.ToList().Count == 0)
                {
                    return GenericResponse<IEnumerable<QueryBookDto>>.Error("Kitap yok");
                }
                return GenericResponse<IEnumerable<QueryBookDto>>.Success("Kitaplar yazar id'sine göre getirildi", bookWithAuthorAsDto);

            }
            catch (Exception ex)
            {

               return GenericResponse<IEnumerable<QueryBookDto>>.Error("HATAA: " + ex.Message);
            }
        }
        public Task<IResponse<UpdateBookDto>> Update(UpdateBookDto book)
        {
            try
            {
                var updatedBooks = _bookRepository.GetByIdAsync(book.Id).Result;
                if (updatedBooks is null)
                {
                    return Task.FromResult<IResponse<UpdateBookDto>>(GenericResponse<UpdateBookDto>.Error("Güncellenecek Kitap Bulunamadı"));
                }
                
                if(!string.IsNullOrEmpty(book.Title))
                {
                    updatedBooks.Title = book.Title;
                }
                if (!string.IsNullOrEmpty(book.Description))
                {
                    updatedBooks.Description = book.Description;
                }
                if(book.PageCount != null)
                {
                    updatedBooks.PageCount = book.PageCount.Value;
                }
                if (book.CategoryId != null)
                {
                    updatedBooks.CategoryId = book.CategoryId.Value;
                }
                if (book.AuthorId != null)
                {
                    updatedBooks.AuthorId = book.AuthorId.Value;
                }
                _bookRepository.Update(updatedBooks);
                _logger.LogInformation("Kitap başarıyla güncellendi: {Title}", book.Title);
                return Task.FromResult<IResponse<UpdateBookDto>>(GenericResponse<UpdateBookDto>.Success("Kitap başarıyla güncellendi"));
            }
            catch (Exception ex)
            {

                return Task.FromResult<IResponse<UpdateBookDto>>(GenericResponse<UpdateBookDto>.Error("HATAA: " + ex.Message));
            }
        }

        
    }
}
