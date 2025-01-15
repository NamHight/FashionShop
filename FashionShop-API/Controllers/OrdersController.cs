using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Extensions;
using FashionShop_API.Models;
using FashionShop_API.Repositories;
using FashionShop_API.Services.ServiceLogger;
using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
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
        const string cartKey = "myCart";
        public List<Cart> Carts => HttpContext.Session.Get<List<Cart>>(cartKey) ?? new List<Cart>();

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
        [HttpPost("ordercancel")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> OrderCancel([FromBody] RequestOrderCancelDto ordercancelsto)
        {
            await _serviceManager.Orders.OrderCancel(ordercancelsto, true);
            return Ok();
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

        [HttpPost("addOrders")]
        public async Task<IActionResult> AddOrder([FromBody] RequestOrderDto order)
        {
            if (order is null) return BadRequest();

            var orderDomain = await _serviceManager.Orders.AddOrder(order);
            foreach (var item in Carts)
            {
                await _serviceManager.Orderdetails.AddOrderDetail(new Ordersdetail
                {
                    Quantity = item.Quantity,
                    TotalPrice = (item.Price * item.Quantity),
                    OrderId = orderDomain.OrderId,
                    ProductId = item.ProductId,
                    Status = "pending",
                });
            }
            return Ok(new {message = "Thanh Toán Thành Công", data = orderDomain});
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
