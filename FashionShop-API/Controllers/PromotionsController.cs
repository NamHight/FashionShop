using FashionShop_API.Dto.QueryParam;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Exceptions;
using FashionShop_API.Models;
using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FashionShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly IServiceManager _servicesManager;
        private readonly ILogger<PromotionsController> _logger;

        public PromotionsController(IServiceManager servicesManager, ILogger<PromotionsController> logger)
        {
            _servicesManager = servicesManager;
            _logger = logger;
        }

        // GET: api/<PromotionsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponsePromotionDto>>> GetPromotionAsync([FromQuery]ParamPromotionDto paramPromotionDto)
        {
            if(paramPromotionDto.Page < 1)
            {
                throw new PageNotFoundException(paramPromotionDto.Page.ToString());
            }
            var promotions = await _servicesManager.Promotion.GetAllPaginateAsync(paramPromotionDto.Page, paramPromotionDto.Limit);
            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(promotions.page));
            return Ok(promotions.data);
        }
        
        // GET api/<PromotionsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponsePromotionDto>> GetByIdAsync(int id)
        {
            if(id < 1)
            {
                return BadRequest();
            }
            try
            {
                var promotion = await _servicesManager.Promotion.GetByIdPromotionAsync(id, trackChanges: false);
                return Ok(promotion);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new PromotionNotFoundException(id);
            }
            
        }

        
    }
}
