using FashionShop_API.Dto;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Extensions;
using FashionShop_API.Mappers;
using FashionShop_API.Models;
using FashionShop_API.Repositories.Shared;
using FashionShop_API.Services.Caching;
using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace FashionShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly IServiceManager _serviceManager;
        private readonly MapperProfile _mapper;
        const string cartKey = "myCart";
        public List<Cart> Carts => HttpContext.Session.Get<List<Cart>>(cartKey) ?? new List<Cart>();
        public CartsController(ILogger<CategoriesController> logger, IServiceManager serviceManager, IServiceCacheRedis serviceCacheRedis) {
            _logger = logger;
            _serviceManager = serviceManager;
        }

       
        [HttpGet("getAllCarts")]
        public IActionResult GetAllCart() {
            Console.WriteLine("danh sach cart la", Carts);
            return Ok(Carts);
        }

        [HttpGet("getPaginationAllCarts/{page}")]
        public IActionResult GetCartPaginationAsync(int page)
        {
            var result = new PaginationCart();
            result.PageSize =  3;
            result.TotalCount = Carts.Count();
            result.TotalPages = (result.TotalCount % result.PageSize)>0 ? (result.TotalCount / result.PageSize) +1 : result.TotalCount / result.PageSize;
            result.CurrentPage = page;
            List<Cart> carts = Carts.Skip((page - 1) * result.PageSize).Take(result.PageSize).ToList();
            Console.WriteLine("Lay danh sach cart bang pagination", carts);
            return Ok(new { data = carts, infoPage = result });
        }
      
        [HttpPost("addCart")]
        public async Task<IActionResult> AddCart(long id, int quantity)
        {
            var newCart = Carts; // tạo đối tượng và gán cho nó giá trị của cart hiện tại
            var checkProductInCart = newCart.Find(item => item.ProductId == id); // kiểm tra xem sp có tồn tại trong cart chưa
            if (checkProductInCart == null) // nếu sản phẩm mới thêm chưa tồn tại trong gio hang thì làm....
            {
                var product = await _serviceManager.Product.FindProductByIdAsync(id, false);
                // kiểm tra xem sản phẩm mới thêm vô giỏ có tồn tại trong bảng Product k
                if (product == null)
                {
                    Console.WriteLine("Da vao trang k thay san pham");
                    return NotFound();
                }
                var cartItem = new Cart() {  // tạo đối tượng cart mới để thêm vô cart
                    ProductId = product.ProductId, Banner = product.Banner, ProductName = product.ProductName, Price = product.Price, Quantity = quantity
                };
                newCart.Add(cartItem);
            }
            else // nếu sản phẩm mới thêm đã tồn tại trong giỏ hàng rồi thì....
            {
                checkProductInCart.Quantity += quantity;
            }
            HttpContext.Session.Set(cartKey, newCart); // cài lại cart
            return Ok(newCart);
        }

        [HttpDelete("removeCarts/{id}")]
        public async Task<IActionResult> RemoveCart(long id)
        {
            var newCart = Carts; // tạo đối tượng và gán cho nó giá trị của cart hiện tại
            var checkExistCart = newCart.Find(item => item.ProductId == id);
            if (checkExistCart != null)
            {
                var checkExistProduct = await _serviceManager.Product.FindProductByIdAsync(id, false);
                if (checkExistProduct == null) {
                    return NotFound();
                }
                newCart.Remove(checkExistCart); // xóa sản phẩm khỏi cart
            }
            HttpContext.Session.Set(cartKey, newCart); // cài lại cart
            return Ok();
        }

        [HttpPost("updateCart")]
        public async Task<IActionResult> UpdateCart([FromBody] List< RequestCartDto> requestCart )
        {
            if (requestCart is null)
            {
                return BadRequest(new {Message= "Du lieu null"});
            }
            HttpContext.Session.Set(cartKey, requestCart); // cài lại cart
            return Ok();
        }
    }
}
