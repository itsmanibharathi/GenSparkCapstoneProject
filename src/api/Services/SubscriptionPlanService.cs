using api.Exceptions;
using api.Models.Dtos.SubscriptionDto;
using api.Repositories.Interfaces;
using api.Services.Interfaces;
using AutoMapper;

namespace api.Services
{
    public class SubscriptionPlanService : ISubscriptionPlanService
    {
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;
        private readonly IMapper _mapper;

        public SubscriptionPlanService(ISubscriptionPlanRepository subscriptionPlanRepository,IMapper mapper )
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
            _mapper = mapper;

        }
        public async Task<IEnumerable<ReturnSubscriptionPlanDto>> GetAsync()
        {
            try
            {
                var res = await _subscriptionPlanRepository.GetAsync();
                return _mapper.Map<IEnumerable<ReturnSubscriptionPlanDto>>(res);
            }
            catch (UnableToDoActionException )
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException(e.Message);
            }

        }
    }
}
