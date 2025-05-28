using KutuphaneCore.Entities;
using KutuphaneDataAcces.Dtos.Authors;

namespace KutuphaneService.Interfaces
{
    public interface IAuthorService
    {
         IResponse<IEnumerable<QueryAuthorDto>> ListAll();
         IResponse<QueryAuthorDto> GetById(int id);
         Task<IResponse<Author>> Create(AuthorDto author);
         IResponse<UpdateAuthorDto> Update(UpdateAuthorDto authorUpdate);
         IResponse<Author> Delete(int id);
         IResponse<IEnumerable<QueryAuthorDto>> GetByName(string name);
    }
}
