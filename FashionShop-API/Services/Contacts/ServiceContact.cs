using AutoMapper;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Models;
using FashionShop_API.Repositories;
using FashionShop_API.Services.ServiceLogger;

namespace FashionShop_API.Services.Contacts
{
    public class ServiceContact : IServiceContact
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ServiceContact(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<RequestContactDto> CreateAsync(RequestContactDto request)
        {
            var contact = _mapper.Map<Contact>(request);
            await _repositoryManager.Contact.CreateAsync(contact);
            await _repositoryManager.SaveChanges();
            var contactReturn = _mapper.Map<RequestContactDto>(contact);
            _logger.LogInfo("Service Contact: " + nameof(CreateAsync) + " Success");
            return contactReturn;
        }
    }
}
