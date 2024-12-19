namespace Charlie.Product.DataAccess.Repositories;

public interface IProductRepository<T> where T : class
{
    Task<IEnumerable<T>> GetProductsAsync();
    Task<T> GetProductAsync(int id);
    Task CreateProductAsync(T product);
    Task<T> UpdateProductAsync(T product);
    Task DeleteProductAsync(int id);
}