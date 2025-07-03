using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Exceptions
{
    public sealed class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base($"Data Not Found")
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}
