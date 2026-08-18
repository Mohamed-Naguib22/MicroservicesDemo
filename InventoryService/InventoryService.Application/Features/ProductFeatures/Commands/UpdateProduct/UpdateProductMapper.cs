using Mapster;
using InventoryService.Domain.Entities.ProductEntities;
using InventoryService.Domain.Events;

namespace InventoryService.Application.Features.ProductFeatures.Commands.UpdateProduct
{
    public sealed class UpdateProductMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UpdatedProduct, Product>()
                .IgnoreNullValues(true);
        }
    }
}