using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Filters;
using FashionShop_API.Services.Contacts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FashionShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly IServiceContact _serviceContact;

        public ContactsController(ILogger<ContactsController> logger, IServiceContact serviceContact)
        {
            _logger = logger;
            _serviceContact = serviceContact;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> CreateContact([FromBody] RequestContactDto requestContactDto)
        {
            if (requestContactDto == null) {
                return BadRequest(new { message ="Request not null!"});
            }
            try
            {
                var contact = await _serviceContact.CreateAsync(requestContactDto);
                return Ok("Create Contact susscess.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later!" });
            }
        }
    }
}
