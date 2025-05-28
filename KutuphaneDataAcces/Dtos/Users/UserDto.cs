using KutuphaneCore.Entities;

namespace KutuphaneDataAcces.Dtos.Users
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string? Username { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
    }
}
