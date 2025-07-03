using AutoMapper;
using ProductService.Application.Contract.IInfrastructure.IEventDispatcher;
using ProductService.Application.Mediator.Common;
using ProductService.Domain.Events.ProductEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Features.ProductFeatures.Commands.CreateProduct
{
    public sealed class CreateProductHandler(IEventDispatcher eventDispatcher, IMapper mapper) : BaseEventHandler<ProductCreated, CreateProductRequest>(eventDispatcher, mapper);
}
