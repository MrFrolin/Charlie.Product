using Charlie.Product.Shared.Models;

namespace Charlie.Product.Service;

public interface IProductService
{
    Task<IEnumerable<ProductModel>> GetProductsAsync();
    Task<ProductModel> GetProductAsync(int id);
    Task CreateProductAsync(ProductModel product);
    Task<ProductModel> UpdateProductAsync(ProductModel product);
    Task DeleteProductAsync(int id);

}