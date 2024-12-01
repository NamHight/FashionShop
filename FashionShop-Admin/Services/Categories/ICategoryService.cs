using FashionShop.Models;

namespace FashionShop.Services.Categories;

public interface ICategoryService
{

    Task<List<Category>> GetAllAsync(bool trackChanges);

    Task<long> FindByNameAsync(string newCategoryName);

}