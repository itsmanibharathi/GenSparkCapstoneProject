using api.Models;

namespace api.Repositories.Interfaces
{
    public interface IUserAuthRepository : IRepository<int, UserAuth>
    {
        Task<UserAuth> GetByUserEmailAsync(string userEmail);
    }
}
