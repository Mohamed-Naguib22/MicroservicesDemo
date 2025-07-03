using InventoryService.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InventoryService.Domain.Constants
{
    public static class MongoCollectionNames
    {
        public static string PRODUCT_COLLECTION_NAME { get; } = "products";

        public static string GetCollectionName<T>()
        {
            return typeof(T).Name switch
            {
                nameof(Product) => PRODUCT_COLLECTION_NAME,
                _ => throw new InvalidOperationException($"Collection name for type {typeof(T).Name} is not defined.")
            };
        }
    }
}
