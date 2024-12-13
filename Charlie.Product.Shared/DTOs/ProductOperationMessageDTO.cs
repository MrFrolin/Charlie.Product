namespace Charlie.Product.Shared.DTOs;

public class ProductOperationMessageDTO
{
    public string CorrelationId { get; set; }
    public string Operation { get; set; }
    public ProductDTO Payload { get; set; }
}