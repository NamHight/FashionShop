using FashionShop_API.Repositories;
using FashionShop_API.Services.ServiceLogger;
using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FashionShop_API.Controllers
{
    //[Authorize(Policy = "MultiAuth")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILoggerManager _loggerManager;

        public OrdersController(IServiceManager serviceManager, ILoggerManager loggerManager)
        {
            _serviceManager = serviceManager;
            _loggerManager = loggerManager;
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}/{status}")]
        public async Task<IActionResult> GetListOrdersByIdAndStatus(long? id, string status)
        {
            if(id is null)
            {
                return BadRequest("Id is null");
            }
            _loggerManager.LogInfo("Controller Orders: " + nameof(GetListOrdersByIdAndStatus) + " Success");
            var result = await _serviceManager.Orders.GetListOrdersByIdAndStatus(id, status, false);
            return Ok(result);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpGet]
        public async Task<IActionResult> CanReviewProduct(long customerId, long productId)
        {
            var hasPurchased = await _serviceManager.Orders.HasPurchasedProductAsync(customerId, productId);

            if (hasPurchased)
            {
                return Ok(new { message = "You can review this product." });
            }
            else
            {
                return BadRequest(new { message = "You must purchase this product before reviewing." });
            }
        }
    }
}
