namespace BrewUp.ReadModel.Services;

public interface IPurchaseOrderService
{
	Task CreatePurchaseOrder(PurchaseOrderId purchaseOrderId, DateTime date, IEnumerable<OrderLine> lines, SupplierId supplierId);
	Task UpdateStatusToComplete(PurchaseOrderId purchaseOrderId);
}