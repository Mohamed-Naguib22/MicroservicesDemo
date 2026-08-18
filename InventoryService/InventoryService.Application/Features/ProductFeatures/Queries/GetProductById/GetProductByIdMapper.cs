using Mapster;
using InventoryService.Domain.Entities.ProductEntities;

namespace InventoryService.Application.Features.ProductFeatures.Queries.GetProductById
{
    public sealed class GetProductByIdMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Product, GetProductByIdResponse>();
        }
    }
}