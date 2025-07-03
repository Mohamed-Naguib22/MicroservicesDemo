using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Contract.IInfrastructure.IMessagePublisher
{
    public interface IMessagePublisher
    {
        Task PublishAsync<T>(T message);
    }
}
