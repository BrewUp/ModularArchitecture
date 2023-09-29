using BrewUp.Modules.Purchases.Domain.CommandHandlers;
using BrewUp.Modules.Purchases.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Infrastructure.RabbitMq.Commands;

public class CreatePurchaseOrderConsumer : CommandConsumerBase<CreatePurchaseOrder>
{
    protected override ICommandHandlerAsync<CreatePurchaseOrder> HandlerAsync { get; }

    public CreatePurchaseOrderConsumer(IRepository repository, IMufloneConnectionFactory connectionFactory,
        ILoggerFactory loggerFactory, IEventBus eventBus) : base(repository, connectionFactory, loggerFactory)
    {
        HandlerAsync = new CreatePurchaseOrderCommandHandler(repository, loggerFactory, eventBus);
    }
}