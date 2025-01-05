using AutoMapper;
using FashionShop_API.Exceptions;
using FashionShop_API.Repositories;
using FashionShop_API.Services.ServiceLogger;

namespace FashionShop_API.Services.WebsiteInfos;

public class ServiceWebsiteInfo : IServiceWebsiteInfo
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly IMapper _mapper;
    public ServiceWebsiteInfo(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
        _mapper = mapper;
    }

    public async Task<Dictionary<string, string>> GetWebsiteInfoAsync(bool trackChanges)
    {
        try
        {
           var webisteInfo =  await _repositoryManager.WebsiteInfo.GetWebsiteInfoAsync(trackChanges);
           if (webisteInfo is null)
           {
               _loggerManager.LogError("WebsiteInfo is null");
               throw new WebsiteInfoNotFoundException("WebsiteInfo is null");
           }
           return webisteInfo;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new WebsiteInfoNotFoundException("WebsiteInfo is null");
        }
    } 
    
}