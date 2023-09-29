using BrewUp.Modules.Purchases.Domain.Entities;
using BrewUp.Modules.Purchases.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Persistence;

namespace BrewUp.Modules.Purchases.Domain.CommandHandlers;

public sealed class ChangePurchaseOrderStatusToCompleteCommandHandler : CommandHandlerBaseAsync<ChangePurchaseOrderStatusToComplete>
{
	private readonly IEventBus _eventBus;

	public ChangePurchaseOrderStatusToCompleteCommandHandler(IRepository repository, ILoggerFactory loggerFactory,
		IEventBus eventBus) : base(repository, loggerFactory)
	{
		_eventBus = eventBus;
	}

	public override async Task ProcessCommand(ChangePurchaseOrderStatusToComplete command, CancellationToken cancellationToken = default)
	{
			// Aggregate Factory
			var purchaseOrderStatusCompleted = PurchaseOrder.ChangePurchaseOrderStatusToComplete(command.PurchaseOrderId);
			await _eventBus.PublishAsync(purchaseOrderStatusCompleted, cancellationToken);
	}
}