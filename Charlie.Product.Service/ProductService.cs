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

    public Task<ProductModel> GetProductAsync(int id)
    {
        _logger.LogInformation($"Getting product by id: {id}");
        return _productRepository.GetProductAsync(id);
    }

    public Task<ProductModel> CreateProductAsync(ProductModel product)
    {
        _logger.LogInformation("Creating product");
        return _productRepository.CreateProductAsync(product);
    }

    public Task<ProductModel> UpdateProductAsync(ProductModel product)
    {
        _logger.LogInformation($"Updating product by id: {product.Id}");
        return _productRepository.UpdateProductAsync(product);
    }

    public Task DeleteProductAsync(int id)
    {
        _logger.LogInformation($"Deleting product by id: {id}");
        return _productRepository.DeleteProductAsync(id);
    }
}