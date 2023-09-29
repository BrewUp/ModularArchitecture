﻿using BrewUp.Modules.Purchases.SharedKernel.DomainIds;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using Muflone.Messages.Events;

namespace BrewUp.Modules.Purchases.Messages.Events;

public sealed class PurchaseOrderCreated : DomainEvent
{
    public readonly PurchaseOrderId PurchaseOrderId;
    public readonly SupplierId SupplierId;
    public readonly DateTime Date;
    public readonly IEnumerable<OrderLine> Lines;
    public PurchaseOrderCreated(PurchaseOrderId aggregateId, SupplierId supplierId, DateTime date, IEnumerable<OrderLine> lines) : base(aggregateId)
    {
        PurchaseOrderId = aggregateId;
        SupplierId = supplierId;
        Date = date;
        Lines = lines;
    }
}