
using System.Text.Json;
using Charlie.Product.Service;
using Charlie.Product.Shared.DTOs;
using Charlie.Product.Shared.Mappers;

namespace Charlie.Product.RMQ
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMqClient _rabbitMqClient;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, RabbitMqClient rabbitMqClient, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _rabbitMqClient = rabbitMqClient;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker is subscribing to product.operations queue...");

            await _rabbitMqClient.SubscribeAsync("product.operations", async message =>
            {
                
                try
                {
                    _logger.LogInformation($"Worker received message: {message}");


                    var operation = JsonSerializer.Deserialize<ProductOperationMessageDTO>(message);

                    // Check if operation is not null and has the expected properties
                    if (operation != null)
                    {
                        string correlationId = operation.CorrelationId;
                        string operationType = operation.Operation;
                        var product = operation.Payload;

                        _logger.LogInformation($"Processing operation for CorrelationId: {correlationId}");

                        using (var scope = _serviceScopeFactory.CreateScope())
                        {
                            var productService = scope.ServiceProvider.GetRequiredService<IProductService>();  // Resolve repository
                            var productMapper = scope.ServiceProvider.GetRequiredService<ProductMapper>();  // Resolve mapper

                            if (operationType == "Create")
                            {
                                var productModel = productMapper.ToProductModel(product);
                                await productService.CreateProductAsync(productModel);

                                var response = new ProductResponseDTO
                                {
                                    CorrelationId = correlationId,
                                    Status = "Processed",
                                    Message = "Product created successfully"
                                };
                                await _rabbitMqClient.PublishAsync("product.responses", response);
                                _logger.LogInformation($"Response sent for CorrelationId: {response.CorrelationId}");
                            }
                            else if (operationType == "Read")
                            {
                                var existingProduct = await productService.GetProductAsync(product.Id);

                                ProductDTO productDTO = null;
                                string status = "Failed";
                                string responseMessage = "Product not found";

                                if (existingProduct != null)
                                {
                                    productDTO = productMapper.ToProductDTO(existingProduct);
                                    status = "Processed";
                                    responseMessage = "Product retrieved successfully";
                                }

                                var response = new ProductResponseDTO
                                {
                                    CorrelationId = correlationId,
                                    Status = status,
                                    Message = responseMessage,
                                    Payload = productDTO
                                };
                                await _rabbitMqClient.PublishAsync("product.responses", response);
                                _logger.LogInformation($"Response sent for CorrelationId: {response.CorrelationId} {response.Payload}");
                            }
                            else
                            {
                                var response = new ProductResponseDTO
                                {
                                    CorrelationId = correlationId,
                                    Status = "Failed",
                                    Message = "Unknown operation type"
                                };
                                await _rabbitMqClient.PublishAsync("product.responses", response);
                                _logger.LogWarning($"Unknown operation type for CorrelationId: {correlationId} {response.Payload}");
                            }
                        }
                    }
                    else
                    {
                        _logger.LogWarning("Received an invalid request message (null request).");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error processing message: {ex.Message}");
                }
            }, stoppingToken);
        }
    }
}
