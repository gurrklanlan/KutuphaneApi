using KutuphaneCore.Entities;

namespace KutuphaneDataAcces.Dtos.Categories
{
    public class QueryCategoryDto:BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
