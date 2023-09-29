using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using Muflone.Core;

namespace BrewUp.Modules.Purchases.Domain.Entities;

public class PurchaseLine : Entity
{
    private BeerId _beerId;
    private BeerName _beerName;
    private Quantity _quantity;
    private Price _price;

    protected PurchaseLine()
    {
        _beerId = default!;
        _beerName = default!;
        _quantity = default!;
        _price = default!;
    }

    internal static PurchaseLine CreatePurchaseLine(BeerId beerId, BeerName beerName, Quantity quantity, Price price)
    {
        return new PurchaseLine
        {
            _beerId = beerId,
            _beerName = beerName,
            _quantity = quantity,
            _price = price
        };
    }
}