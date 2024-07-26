
using api.Models.Dtos.UserDtos;

namespace api.Services.Interfaces
{
    public interface IUserService
    {
        public Task<string> Activation(int id,string token);
        public Task<ReturnUserDto> GetUserAsync(int id);
        public Task<ReturnUserDto> UpdateUserAsync(int id, GetUserEditDto user);
    }
}
