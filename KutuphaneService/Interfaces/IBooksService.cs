using KutuphaneCore.Entities;
using KutuphaneDataAcces.Dtos.Books;

namespace KutuphaneService.Interfaces
{
    public interface IBooksService
    {
        IResponse<IEnumerable<QueryBookDto>> ListAll();
        IResponse<QueryBookDto> GetById(int id);
        Task<IResponse<BookDto>> Create(BookDto book);
        Task<IResponse<UpdateBookDto>> Update(UpdateBookDto book);
        IResponse<Book> Delete(int id);
        IResponse<IEnumerable<QueryBookDto>> GetByName(string name);
        IResponse<IEnumerable<QueryBookDto>> GetBooksByCategoryId(int categoryId);
        IResponse<IEnumerable<QueryBookDto>> GetBooksByAuthorId(int authorId);

    }
}
