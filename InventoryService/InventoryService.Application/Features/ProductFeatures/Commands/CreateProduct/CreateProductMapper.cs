using Mapster;
using InventoryService.Domain.Entities.ProductEntities;
using InventoryService.Domain.Events;

namespace InventoryService.Application.Features.ProductFeatures.Commands.CreateProduct
{
    public sealed class CreateProductMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ProductCreatedEvent, Product>();
        }
    }
}