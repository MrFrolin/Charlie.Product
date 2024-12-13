namespace Charlie.Product.DataAccess.Interfaces;

public interface IProductRepository <T> where T : class
{
    Task<IEnumerable<T>> GetProductsAsync();
    Task<T> GetProductAsync(Guid id);
    Task<T> CreateProductAsync(T product);
    Task<T> UpdateProductAsync(T product);
    Task DeleteProductAsync(Guid id);
}