using Charlie.Product.DataAccess.Repositories;
using Charlie.Product.Shared.Models;
using Microsoft.Extensions.Logging;

namespace Charlie.Product.Service;

public class ProductService : IProductService
{
    private readonly IProductRepository<ProductModel> _productRepository;
    private readonly ILogger<ProductService> _logger;


    public async Task<IEnumerable<ProductModel>> GetProductsAsync()
    {
        _logger.LogInformation("Getting all products");
        return await _productRepository.GetProductsAsync();
    }

    public async Task<ProductModel> GetProductAsync(int id)
    {
        _logger.LogInformation($"Getting product by id: {id}");
        return await _productRepository.GetProductAsync(id);
    }

    public async Task CreateProductAsync(ProductModel product)
    {
        _logger.LogInformation("Creating product");
        await _productRepository.CreateProductAsync(product);
    }

    public async Task<ProductModel> UpdateProductAsync(ProductModel product)
    {
        _logger.LogInformation($"Updating product by id: {product.Id}");
        return await _productRepository.UpdateProductAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        _logger.LogInformation($"Deleting product by id: {id}");
        await _productRepository.DeleteProductAsync(id);
    }
}