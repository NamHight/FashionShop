﻿
using FashionShop_API.Context;
using FashionShop_API.Models;
using FashionShop_API.Repositories;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;


namespace FashionShop_API.Repositories.Products
{
    public class RepositoryProduct : RepositoryBase<Product>, IRepositoryProduct
    {
        public RepositoryProduct(MyDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetAllAsync(bool trackChanges)
        {
            var products = await FindAll(trackChanges).OrderByDescending(e => e.ProductId)
				.Include(p => p.Category)
				.ToListAsync();
            return products;
        }

        public async Task<Product?> GetByIdAsync(long id, bool trackChanges)
        {
            var product = await FindByCondition(item => item.ProductId == id, trackChanges).FirstOrDefaultAsync();
            return product;
        }

        public async Task<IEnumerable<Product>?> GetListProductByCategoryId(long categoryId, bool trackChanges)
        {
            var products = await FindByCondition(e=>e.CategoryId == categoryId,trackChanges)
				.ToListAsync();
            return products.Any() ? products : Enumerable.Empty<Product>();
        }

		public async Task<Product> GetProductDetailsAsync(string categorySlug, string productSlug)
		{
			return await _context.Products
				.Include(p => p.Category)
				.Where(p => p.Category.Slug == categorySlug && p.Slug == productSlug)
				.FirstOrDefaultAsync();
		}
		private string _querySearch = @"Select * from products where Match(product_name) against({0} IN BOOLEAN MODE) OR product_name like {1} and status = 'available'";
		public async Task<IEnumerable<Product>> SearchByName(string? searchTerm,string? sortOrder)
		{
			var query = _context.Products;
			if (string.IsNullOrWhiteSpace(searchTerm))
			{
				return Enumerable.Empty<Product>();
			}
			var searchResult = await query
				.FromSqlRaw(_querySearch, searchTerm,$"%{searchTerm}%")
				.SortByCreatedDate(sortOrder)
				.SortByPrice()
				.AsNoTracking()
				.ToListAsync();
			return searchResult.AsEnumerable();
		}
    }
}
