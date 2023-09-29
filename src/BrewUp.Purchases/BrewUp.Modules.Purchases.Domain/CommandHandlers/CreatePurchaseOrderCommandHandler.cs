using BrewUp.Modules.Purchases.Domain.Entities;
using BrewUp.Modules.Purchases.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Persistence;

namespace BrewUp.Modules.Purchases.Domain.CommandHandlers;

public sealed class CreatePurchaseOrderCommandHandler : CommandHandlerBaseAsync<CreatePurchaseOrder>
{
	private readonly IEventBus _eventBus;

	public CreatePurchaseOrderCommandHandler(IRepository repository, ILoggerFactory loggerFactory, IEventBus eventBus) : base(repository, loggerFactory)
	{
		_eventBus = eventBus;
	}

	public override async Task ProcessCommand(CreatePurchaseOrder command, CancellationToken cancellationToken = default)
	{
		// Do something with the command
		var purchaseOrderCreated = PurchaseOrder.RaisePurchaseOrderCreated(command.PurchaseOrderId, command.SupplierId, command.Date, command.Lines);
		await _eventBus.PublishAsync(purchaseOrderCreated, cancellationToken);
	}
}