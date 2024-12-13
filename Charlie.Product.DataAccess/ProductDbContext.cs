using Microsoft.EntityFrameworkCore;
using Charlie.Product.DataAccess.Models;

namespace Charlie.Product.DataAccess
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }

        public DbSet<ProductModel> Products { get; set; }
    }
}
