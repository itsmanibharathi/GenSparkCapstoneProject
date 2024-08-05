using api.Exceptions;
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
                //var to = property.User.UserEmail;
                //var subject = "New Contact";
                //var body = $"You have been contacted by {user.UserEmail}";
                //return await _azureMailService.Send(to, subject, body);

                // Sending email to the property owner
                var ownerEmail = property.User.UserEmail;
                var ownerSubject = $"New Contact on Your Property |  {property.Title}";
                var ownerBody = $"<div> Hello {property.User.UserName},\n\nYou have been contacted by {user.UserName}, Email {user.UserEmail}, Phone {user.UserPhoneNumber} regarding your property {property.Title}.\n\nBest regards,\n360area.tech<div> ";

                await _azureMailService.Send(ownerEmail, ownerSubject, ownerBody);

                // Sending email to the buyer
                var buyerEmail = user.UserEmail;
                var buyerSubject = $"Contact Confirmation | {property.Title}";
                var buyerBody = $"Hello {user.UserName},\n\nYou have successfully contacted the owner of the property {property.Title}. The Property Owner Contact \n\nBest regards,\n 360area.tech ";

                await _azureMailService.Send(buyerEmail, buyerSubject, buyerBody);
                return true;
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
                //property.User.UserSubscriptionPlan.Append(userSubscriptionPlan);
                property.ViewCount++;
                await _userSubscriptionPlanRepository.UpdateAsync(userSubscriptionPlan);
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

                // Sending email to the property owner
                var ownerEmail = property.User.UserEmail;
                var ownerSubject = $"Your Property Info Viewed | {property.Title}";
                var ownerBody = $"Hello {property.User.UserName},\n\nYour property {property.Title} information has been viewed by {user.UserEmail}.\n\nBest regards,\n360area.tech";

                await _azureMailService.Send(ownerEmail, ownerSubject, ownerBody);

                // Sending email to the user
                var userEmail = user.UserEmail;
                var userSubject = $"Owner Info Viewed | {property.Title}";
                var userBody = $"Hello {user.UserName},\n\nYou have viewed the owner's information for the property {property.Title}. The owner's Name is {property.User.UserName}, Email {property.User.UserEmail}, Phone {property.User.UserEmail}.\n\nBest regards,\n360area.tech";

                await _azureMailService.Send(userEmail, userSubject, userBody);

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
