using api.Exceptions;
using api.Models;
using api.Models.Dtos.SubscriptionDto;
using api.Models.Enums;
using api.Repositories;
using api.Repositories.Interfaces;
using api.Services.Interfaces;
using AutoMapper;

namespace api.Services
{
    public class UserSubscriptionPlanService : IUserSubscriptionPlanService
    {
        private readonly IMapper _mapper;
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;
        private readonly IUserSubscriptionPlanRepository _userSubscriptionPlanRepository;
        private readonly ITokenService<UserSubscriptionPlan> _userSubscriptionPlanTokenService;

        public UserSubscriptionPlanService(ISubscriptionPlanRepository subscriptionPlanRepository, IUserSubscriptionPlanRepository userSubscriptionPlanRepository, ITokenService<UserSubscriptionPlan> tokenService,IMapper mapper)
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
            _userSubscriptionPlanRepository = userSubscriptionPlanRepository;
            _userSubscriptionPlanTokenService = tokenService;
            _mapper = mapper;
        }
        public async Task<ReturnUserSubscriptionPlanDto> SubscribeAsync(int userId, int subscriptionPlanId)
        {
            try
            {
                if (await _userSubscriptionPlanRepository.UserUseFreePlanAsync(userId,subscriptionPlanId) ||
                    await _userSubscriptionPlanRepository.UserHasActivePlanAsync(userId,subscriptionPlanId))
                {
                    throw new InvalidOperationException("User already has active plan or used free plan");
                }
                var subcriptionPlan = await _subscriptionPlanRepository.GetAsync(subscriptionPlanId);

                var userSubscriptionPlan = new UserSubscriptionPlan
                {
                    UserId = userId,
                    SubscriptionPlanId = subscriptionPlanId,
                    SubscriptionStartDate = DateTime.Now,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                };
                if(subcriptionPlan.SubscriptionPlanDurationType == SubscriptionPlanDurationType.Days)
                {
                    userSubscriptionPlan.SubscriptionEndDate = DateTime.Now.AddMonths(subcriptionPlan.SubscriptionPlanDuration);
                }
                if (subcriptionPlan.SubscriptionPlanDurationType == SubscriptionPlanDurationType.Count)
                {
                    userSubscriptionPlan.AvailableSellerViewCount = subcriptionPlan.SubscriptionPlanDuration;
                }
                var res =await _userSubscriptionPlanRepository.AddAsync(userSubscriptionPlan);
                var result = _mapper.Map<ReturnUserSubscriptionPlanDto>(res);
                result.SubscriptionPlanName = subcriptionPlan.SubscriptionPlanName;
                result.SubscriptionPlanDescription = subcriptionPlan.SubscriptionPlanDescription;
                result.SubscriptionPlanPrice = subcriptionPlan.SubscriptionPlanPrice;
                result.SubscriptionPlanDuration = subcriptionPlan.SubscriptionPlanDuration;
                result.SubscriptionPlanDurationType = subcriptionPlan.SubscriptionPlanDurationType;
                return result;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (EntityNotFoundException<SubscriptionPlan>)
            {
                throw;
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to subscribe user to plan", e);
            }
        }
    }
}
