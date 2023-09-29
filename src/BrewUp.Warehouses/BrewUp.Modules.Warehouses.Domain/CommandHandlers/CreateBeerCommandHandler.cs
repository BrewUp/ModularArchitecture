using BrewUp.Shared.Commands;
using BrewUp.Shared.Events;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Persistence;

namespace BrewUp.Modules.Warehouses.Domain.CommandHandlers;

public sealed class CreateBeerCommandHandler : CommandHandlerBaseAsync<CreateBeer>
{
	private readonly IEventBus _eventBus;
	public CreateBeerCommandHandler(IEventBus eventBus, IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
	{
		_eventBus = eventBus;
	}

	public override async Task ProcessCommand(CreateBeer command, CancellationToken cancellationToken = default)
	{
		var beerCreated = new BeerCreated(command.BeerId, command.BeerName);
		await _eventBus.PublishAsync(beerCreated, cancellationToken);
	}
}