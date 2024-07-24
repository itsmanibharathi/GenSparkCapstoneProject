using api.Models.Dtos.UserDtos;

namespace api.Services.Interfaces
{
    public interface IUserAuthService
    {
        Task<ReturnUserLoginDto> Login(GetUserLoginDto getUserLoginDto);
        Task<ReturnUserRegisterDto> Register(GetUserRegisterDto getUserRegisterDto);
    }
}
