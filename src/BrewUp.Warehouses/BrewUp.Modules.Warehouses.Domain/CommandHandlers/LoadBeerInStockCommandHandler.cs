using BrewUp.Shared.Commands;
using BrewUp.Shared.Events;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Persistence;

namespace BrewUp.Modules.Warehouses.Domain.CommandHandlers;

public sealed class LoadBeerInStockCommandHandler : CommandHandlerBaseAsync<LoadBeerInStock>
{
	private readonly IEventBus _eventBus;

	public LoadBeerInStockCommandHandler(IEventBus eventBus, IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
	{
		_eventBus = eventBus;
	}

	public override async Task ProcessCommand(LoadBeerInStock command, CancellationToken cancellationToken = default)
	{
		var beerLoadedInStock = new BeerLoadedInStock(command.BeerId, command.Stock, command.Price, command.PurchaseOrderId);
		await _eventBus.PublishAsync(beerLoadedInStock, cancellationToken);
	}
}