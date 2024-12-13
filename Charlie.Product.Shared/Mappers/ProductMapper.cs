using Charlie.Product.Shared.DTOs;
using Charlie.Product.Shared.Models;

namespace Charlie.Product.Shared.Mappers;

public class ProductMapper
{
    public ProductModel ToProductModel(ProductDTO productDTO)
    {

        return new ProductModel
        {
            Id = productDTO.Id,
            Name = productDTO.Name,
            Price = productDTO.Price

        };
    }

    public ProductDTO ToProductDTO(ProductModel productModel)
    {
        return new ProductDTO
        {
            Id = productModel.Id,
            Name = productModel.Name,
            Price = productModel.Price
        };
    }
}