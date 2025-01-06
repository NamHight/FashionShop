using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Filters;
using FashionShop_API.Services.Contacts;
using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FashionShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly IServiceManager _serviceManager;

        public ContactsController(ILogger<ContactsController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> CreateContact([FromBody] RequestContactDto requestContactDto)
        {
            if (requestContactDto == null) {
                return BadRequest();
            }
            try
            {
                var contact = await _serviceManager.Contact.CreateAsync(requestContactDto);
                return Ok("Gửi liên hệ thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later!" });
            }
        }
    }
}
