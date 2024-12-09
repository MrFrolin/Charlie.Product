namespace Charlie.Product.Service;

public interface IProductService <T> where T : class
{
    Task<IEnumerable<T>> GetProductsAsync();
    Task<T> GetProductAsync(Guid id);
    Task<T> CreateProductAsync(T product);
    Task<T> UpdateProductAsync(Guid id, T product);
    Task DeleteProductAsync(Guid id);
}