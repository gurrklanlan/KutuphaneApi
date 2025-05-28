using KutuphaneCore.Entities;

namespace KutuphaneDataAcces.Dtos.Authors
{
    public class QueryAuthorDto:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PlaceofBirth { get; set; }
        public int YearOfBirth { get; set; }
    }
}
