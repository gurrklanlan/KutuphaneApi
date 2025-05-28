namespace KutuphaneCore.Entities
{
    public class Book : BaseEntity
    {
       
        public string Title { get; set; }
        public string? Description { get; set; }
        public int PageCount { get; set; }

        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
    }
}
