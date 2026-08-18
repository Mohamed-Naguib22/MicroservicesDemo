using Mapster;
using InventoryService.Domain.Entities.ProductEntities;

namespace InventoryService.Application.Features.ProductFeatures.Queries.GetAllProducts
{
    public sealed class GetAllProductsMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Product, GetAllProductsResponse>();
        }
    }
}