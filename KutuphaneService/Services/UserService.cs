using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using KutuphaneCore.Entities;
using KutuphaneDataAcces.Dtos.Users;
using KutuphaneDataAcces.Repository;
using KutuphaneService.Interfaces;
using KutuphaneService.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace KutuphaneService.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IGenericRepository<User> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public IResponse<UserDto> CreateUser(UserDto userDto)
        {
            if(userDto is null)
            {
                GenericResponse<UserDto>.Error("User data cannot be null");
            }


            if(string.IsNullOrWhiteSpace(userDto.Name) && string.IsNullOrWhiteSpace(userDto.Email))
            {
                return GenericResponse<UserDto>.Error("Name or Email cannot be empty");
            }

            var existingUser = _userRepository.GetAll().FirstOrDefault(u => u.Email == userDto.Email || u.Username == userDto.Username);
            if (existingUser != null)
            {
                return GenericResponse<UserDto>.Error("User with this email or username already exists");
            }

            var hashedPassword =HashPassword(userDto.Password);

            var newUser = new User
            {
                Name = userDto.Name,
                Surname = userDto.Surname,
                Email = userDto.Email,
                Password = hashedPassword, // Ensure to hash the password in a real application
                
            };
            newUser.RecordTime = DateTime.Now;

            _userRepository.Create(newUser);
            return GenericResponse<UserDto>.Success("User created successfully",null);
        }

        public IResponse<string> LoginUser(LoginUserDto loginUserDto)
        {
            if (loginUserDto.Username is null || loginUserDto.Email is null && loginUserDto.Password is null)
            {
                return GenericResponse<string>.Error("Username, Email or Password cannot be null");
            }


            var checkUser = _userRepository.GetAll().FirstOrDefault(u => (u.Username == loginUserDto.Username ||
            u.Email == loginUserDto.Email) &&
            u.Password == HashPassword(loginUserDto.Password));

            

            if (checkUser is null)
            {
                return GenericResponse<string>.Error("User not found or password is incorrect");
            }

            var checkedLogin = GenerateJwtToken(checkUser);

            return GenericResponse<string>.Success("Login successful", checkedLogin);
        }

        private string HashPassword(string password)
        {
            string secretKey = "Ppg%U{7>s3XWYg<*&b_*TQQvFL9PSX";

            using (var sha256=SHA256.Create())
            {
                var combinedPassword = password + secretKey;

                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedPassword));
                var hashedPassword = Convert.ToBase64String(bytes);
                return hashedPassword;
            }
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                           issuer: _configuration["Jwt:Issuer"],
                                        audience: _configuration["Jwt:Audience"],
                                                   claims:claims,
                                                            expires:DateTime.Now.AddMinutes(1),
                                                                      signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

   
}
