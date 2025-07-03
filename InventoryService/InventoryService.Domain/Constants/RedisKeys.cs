using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Domain.Constants
{
    public static class RedisKeys
    {
        public static string PRODUCTS_KEY { get; } = "_products";
    }
}
