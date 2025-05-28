using KutuphaneCore.Entities;
using KutuphaneDataAcces.Dtos.Users;

namespace KutuphaneService.Interfaces
{
    public interface IUserService
    {
        IResponse<UserDto> CreateUser(UserDto userDto);
        IResponse<string> LoginUser(LoginUserDto loginUserDto);
    }
}
