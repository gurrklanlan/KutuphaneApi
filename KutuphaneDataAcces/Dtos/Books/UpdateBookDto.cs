using KutuphaneCore.Entities;

namespace KutuphaneDataAcces.Dtos.Books
{
    public class UpdateBookDto 
    {
        public int Id { get; set; } 
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? PageCount { get; set; }

        public int? CategoryId { get; set; }
        public int? AuthorId { get; set; }
    }
}
