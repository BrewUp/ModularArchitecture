using BrewUp.Modules.Purchases.ReadModel.Entities;
using BrewUp.Modules.Purchases.SharedKernel.DomainIds;
using BrewUp.Modules.Purchases.SharedKernel.Dtos;
using BrewUp.ReadModel;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using Microsoft.Extensions.Logging;

namespace BrewUp.Modules.Purchases.ReadModel.Services;

public class PurchaseOrderService : ServiceBase, IPurchaseOrderService
{
	public PurchaseOrderService(ILoggerFactory loggerFactory, IPersister persister) : base(loggerFactory, persister)
	{
	}

	public async Task CreatePurchaseOrder(PurchaseOrderId purchaseOrderId, DateTime date, IEnumerable<OrderLine> lines,
		SupplierId supplierId)
	{
		var order = PurchaseOrder.Create(purchaseOrderId, date, lines, supplierId);

		await Persister.InsertAsync(order, CancellationToken.None);
	}

	public async Task UpdateStatusToComplete(PurchaseOrderId purchaseOrderId)
	{
		var order = await Persister.GetByIdAsync<PurchaseOrder>(purchaseOrderId.ToString(), CancellationToken.None);
		order.Status = Status.Complete;
		await Persister.UpdateAsync(order, CancellationToken.None);
	}
}