namespace Charlie.Product.Shared.DTOs;

public class ProductResponseDTO
{
    public string CorrelationId { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public ProductDTO? Payload { get; set; }
}