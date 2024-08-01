using api.Exceptions;
using api.Models;
using api.Models.Dtos.IUserPropertyInteractionDto;
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
        private readonly IAzureMailService _azureMailService;
        private readonly IMapper _mapper;

        public UserPropertyInteractionService(IUserPropertyInteractionRepository userPropertyInteractionRepository, IUserRepository userRepository,IPropertyRepository propertyRepository, IAzureMailService azureMailService, IMapper mapper ) 
        {
            _userPropertyInteractionRepository = userPropertyInteractionRepository;
            _userRepository = userRepository;
            _propertyRepository = propertyRepository;
            _azureMailService = azureMailService;
            _mapper = mapper;
        }
        public async Task<bool> Contact(int userId, int propertyId)
        {
            try
            {
                var user = await _userRepository.GetAsync(userId);
                var property = await _propertyRepository.GetWithOwnerInfoAsync(propertyId);
                if(property.User.UserId == userId)
                {
                    throw new InvalidOperationException("You can't contact yourself");
                }
                var userPropertyInteraction = new UserPropertyInteraction
                {
                    UserId = userId,
                    PropertyId = propertyId,
                    InteractionType = PropertyInteractionsType.Contact
                };
                await _userPropertyInteractionRepository.AddAsync(userPropertyInteraction);
                var to = property.User.UserEmail;
                var subject = "New Contact";
                var body = $"You have been contacted by {user.UserEmail}";
                return await _azureMailService.Send(to, subject, body);
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
                var property = await _propertyRepository.GetWithOwnerInfoAsync(propertyId);
                if (property.User.UserId == userId)
                {
                    throw new InvalidOperationException("You can't contact yourself");
                }
                var userPropertyInteraction =new UserPropertyInteraction
                {
                    UserId = userId,
                    PropertyId = propertyId,
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
