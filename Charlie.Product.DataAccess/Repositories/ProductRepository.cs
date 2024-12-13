using Charlie.Product.DataAccess.Interfaces;
using Charlie.Product.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Charlie.Product.DataAccess.Repositories;

public class ProductRepository : IProductRepository<ProductModel>
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductModel>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<ProductModel?> GetProductAsync(Guid id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<ProductModel> CreateProductAsync(ProductModel product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<ProductModel> UpdateProductAsync(ProductModel product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await GetProductAsync(id);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}