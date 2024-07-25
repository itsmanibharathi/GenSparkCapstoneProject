
namespace api.Services.Interfaces
{
    public interface IUserService
    {
        public Task<string> Activation(int id,string token);
    }
}
