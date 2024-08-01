using api.Models.Dtos.IUserPropertyInteractionDto;

namespace api.Services.Interfaces
{
    public interface IUserPropertyInteractionService
    {
        public Task<bool> Contact(int userId,int propertyId);
        public Task<BuyerViewOwnerInfoDto> ViewOwnerInfo(int userId, int propertyId);
    }
}
