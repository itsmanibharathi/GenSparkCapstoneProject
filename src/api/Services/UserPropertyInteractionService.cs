﻿using api.Exceptions;
using api.Models;
using api.Models.Dtos.UserPropertyInteractionDto;
using api.Models.Enums;
using api.Repositories;
using api.Repositories.Interfaces;
using api.Services.Interfaces;
using AutoMapper;

namespace api.Services
{
    public class UserPropertyInteractionService : IUserPropertyInteractionService
    {
        private readonly IUserPropertyInteractionRepository _userPropertyInteractionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUserSubscriptionPlanRepository _userSubscriptionPlanRepository;
        private readonly IAzureMailService _azureMailService;
        private readonly IMapper _mapper;

        public UserPropertyInteractionService(IUserPropertyInteractionRepository userPropertyInteractionRepository, IUserRepository userRepository,IPropertyRepository propertyRepository, IAzureMailService azureMailService, IUserSubscriptionPlanRepository userSubscriptionPlanRepository, IMapper mapper ) 
        {
            _userPropertyInteractionRepository = userPropertyInteractionRepository;
            _userRepository = userRepository;
            _propertyRepository = propertyRepository;
            _userSubscriptionPlanRepository = userSubscriptionPlanRepository;
            _azureMailService = azureMailService;
            _mapper = mapper;
        }
        public async Task<bool> Contact(int userId, int propertyId)
        {
            try
            {
                var user = await _userRepository.GetAsync(userId);
                var userSubscriptionPlan = await _userSubscriptionPlanRepository.UserSubscriptionPlanAsync(userId, SubscriptionPlanDurationType.Days);
                if (userSubscriptionPlan.SubscriptionEndDate < DateTime.Now)
                {
                    userSubscriptionPlan.IsActive = false;
                    await _userSubscriptionPlanRepository.UpdateAsync(userSubscriptionPlan);
                    throw new InvalidOperationException("Your subscription has expired");
                }
                var property = await _propertyRepository.GetWithOwnerInfoAsync(propertyId);
                property.ViewCount++;
                if (property.User.UserId == userId)
                {
                    throw new InvalidOperationException("You can't contact yourself");
                }
                var userPropertyInteraction = new UserPropertyInteraction
                {
                    UserId = userId,
                    PropertyId = propertyId,
                    InteractionType = PropertyInteractionsType.Contact,
                    Property = property,

                };
                await _userPropertyInteractionRepository.AddAsync(userPropertyInteraction);
                var to = property.User.UserEmail;
                var subject = "New Contact";
                var body = $"You have been contacted by {user.UserEmail}";
                return await _azureMailService.Send(to, subject, body);
            }
            catch (EntityNotFoundException<UserSubscriptionPlan>)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch(EntityAlreadyExistsException<UserPropertyInteraction> )
            {
                throw;
            }
            catch (EntityNotFoundException<User>)
            {
                throw;
            }
            catch (EntityNotFoundException<Property>)
            {
                throw;
            }
            catch(EnvironmentVariableUndefinedException)
            {
                throw;
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to contact", e);
            }
        }
        

        public async Task<BuyerViewOwnerInfoDto> ViewOwnerInfo(int userId, int propertyId)
        {
            try
            {
                var user = await _userRepository.GetAsync(userId);
                var userSubscriptionPlan = await _userSubscriptionPlanRepository.UserSubscriptionPlanAsync(userId, SubscriptionPlanDurationType.Count);
                if (userSubscriptionPlan.AvailableSellerViewCount <= 0)
                {
                    userSubscriptionPlan.IsActive = false;
                    await _userSubscriptionPlanRepository.UpdateAsync(userSubscriptionPlan);
                    throw new InvalidOperationException("You have no more view count");
                    return null;
                }
                var property = await _propertyRepository.GetWithOwnerInfoAsync(propertyId);
                if (property.User.UserId == userId)
                {
                    throw new InvalidOperationException("You can't contact yourself");
                    return null;
                }
                userSubscriptionPlan.AvailableSellerViewCount--;
                if (userSubscriptionPlan.AvailableSellerViewCount == 0)
                {
                    userSubscriptionPlan.IsActive = false;
                }
                property.User.UserSubscriptionPlan.Append(userSubscriptionPlan);
                property.ViewCount++;
                var userPropertyInteraction =new UserPropertyInteraction
                {
                    UserId = userId,
                    PropertyId = propertyId,
                    Property = property,
                    InteractionType = PropertyInteractionsType.ViewOwnerInfo
                };
                try
                {
                    await _userPropertyInteractionRepository.AddAsync(userPropertyInteraction);
                }
                catch (EntityAlreadyExistsException<UserPropertyInteraction>)
                {
                    return _mapper.Map<BuyerViewOwnerInfoDto>(property.User);
                }
                var to = property.User.UserEmail;
                var subject = "Some One View your Post";
                var body = $"You have been contacted by {user.UserEmail}";
                await _azureMailService.Send(to, subject, body);
                return _mapper.Map<BuyerViewOwnerInfoDto>(property.User);
            }
            catch (EntityNotFoundException<UserSubscriptionPlan>)
            {
                throw;
            }
            catch (EntityAlreadyExistsException<UserPropertyInteraction>)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (EntityNotFoundException<User>)
            {
                throw;
            }
            catch (EntityNotFoundException<Property>)
            {
                throw;
            }
            catch (EnvironmentVariableUndefinedException)
            {
                throw;
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to contact", e);
            }
        }
    }
}
