namespace KutuphaneDataAcces.Dtos.Authors
{
    public class UpdateAuthorDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PlaceofBirth { get; set; }
        public int YearOfBirth { get; set; }
    }
}
