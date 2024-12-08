using FashionShop.Models;
using FashionShop.Models.views.ProductViewModels;
using FashionShop.Repositories.ManagerRepository;
using FashionShop.Models.views;

namespace FashionShop.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IManagerRepository _managerRepository;
        private IWebHostEnvironment hostingEnvironment; //xử lý đường dẫn ảnh
        public ProductService(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public async Task<string> HandleUploadImage(IWebHostEnvironment hostingEnvironment, IFormFile? Banner)
        {
            string fileName = "";
            if (Banner != null && Banner.Length > 0) // xử lý lưu ảnh nếu ảnh tồn tại
            {
                // Lưu file: hostingEnvironment.WebRootPath = localhost:3000/wwwroot
                var fullpath = Path.Combine(hostingEnvironment.WebRootPath, "uploaded"); // fullpath = localhost:3000/wwwroot/updloaded
                using (var stream = new FileStream(Path.Combine(fullpath, Banner.FileName), FileMode.Create))
                {
                    await Banner.CopyToAsync(stream); // lưu ảnh vào localhost:3000/wwwroot/updloaded/fileName
                }
                fileName = Banner.FileName; // lưu tên ảnh vào database
            }
            else
            {
                Console.WriteLine("anh chua ton tai");
                fileName = "";
            }
            return fileName;
        }
        public async Task<List<Product>> GetAllAsync(bool trackChanges)
        {
            var products = await _managerRepository.Product.GetAllAsync(trackChanges);
            return products;
        }

        public async Task AddNewProduct(Product product)
        {
            _managerRepository.Product.AddNewProductAsync(product);
            await _managerRepository.SaveAsync();
        }

        public async Task<bool> CheckSlug(string slug)
        {
            var result = await _managerRepository.Product.CheckSlugAsync(slug);
            if (result) return true;
            return false;
        }

        public async Task<bool> UpdateCategoryId(long newCategoryID, long idProduct, bool trackChanges)
        {
            await _managerRepository.Product.UpdateCategoryIdAsync(newCategoryID, idProduct, trackChanges);
            int numRowEffect = await _managerRepository.SaveAsyncAndNumRowEffect();
            Console.WriteLine($"so dong bi anh huong la {numRowEffect}");
            if (numRowEffect > 0) return true;
            return false;
        }

        public async Task<bool> UpdateStatus(string newData, long idProduct, bool trackChanges)
        {
            await _managerRepository.Product.UpdateStatusAsync(newData, idProduct, trackChanges);
            int numRowEffect = await _managerRepository.SaveAsyncAndNumRowEffect();
            if (numRowEffect > 0) return true;
            return false;
        }

        public async Task<Product?> GetByIdAsync(long id, bool trackChanges)
        {
            var product = await _managerRepository.Product.GetByIdAsync(id, trackChanges);
            return product;
        }

        public async Task<bool> UpdateProductAsync(int id, UpdateProductViewModel abc, string image)
        {
            var product = await _managerRepository.Product.GetByIdAsync(id, false);
            if (image != "")
            {
                product.Banner = image;
            }
            product.Status = abc.Status;
            product.ProductName = abc.ProductName;
            product.Slug = abc.Slug;
            product.Price = abc.Price;
            product.Description = abc.Description;
            product.CategoryId = await _managerRepository.Category.FindByNameAsync(abc.CategoryName);
            _managerRepository.Product.UpdateProduct(product);
            int numRowEffect = await _managerRepository.SaveAsyncAndNumRowEffect();
            if (numRowEffect > 0) return true;
            return false;
        }
        public async Task<ProductViewModel> GetPageLinkAsync(string nameSearch, int page, int pageSize, bool trackChanges)
        {
            var productList = await _managerRepository.Product.GetPageLinkAsync(page, pageSize, nameSearch, trackChanges);
            var productCounts = await _managerRepository.Product.GetCountAsync(nameSearch, trackChanges);
            var categories = await _managerRepository.Category.GetAllAsync(trackChanges);
            var result = new ProductViewModel
            {
                Products = productList,
                Categories = categories,
                NameSearch = nameSearch,
                PagingInfo = new PagingInfo
                {
                    TotalItems = productCounts,
                    ItemsPerPage = pageSize,
                    CurrentPage = page,
                }
            };
            return result;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            Product product = await _managerRepository.Product.GetByIdAsync(productId, false);
            _managerRepository.Product.DeleteProduct(product);
            int numRowEffect = await _managerRepository.SaveAsyncAndNumRowEffect();
            if (numRowEffect > 0) return true;
            return false;
        }
    }
}
