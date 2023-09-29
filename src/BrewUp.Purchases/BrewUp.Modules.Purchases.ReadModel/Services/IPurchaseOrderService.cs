using BrewUp.Modules.Purchases.SharedKernel.DomainIds;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;

namespace BrewUp.Modules.Purchases.ReadModel.Services;

public interface IPurchaseOrderService
{
	Task CreatePurchaseOrder(PurchaseOrderId purchaseOrderId, DateTime date, IEnumerable<OrderLine> lines, SupplierId supplierId);
	Task UpdateStatusToComplete(PurchaseOrderId purchaseOrderId);
}