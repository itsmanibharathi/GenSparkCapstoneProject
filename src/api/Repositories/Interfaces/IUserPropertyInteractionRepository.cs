using api.Models;

namespace api.Repositories.Interfaces
{
    public interface IUserPropertyInteractionRepository : IRepository<int,UserPropertyInteraction>
    {
        public Task<IEnumerable<UserPropertyInteraction>> GetByUserId(int userId);
    }

}
