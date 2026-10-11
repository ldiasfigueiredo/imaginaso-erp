
namespace ImaginaSo.Application.Pricing
{
    public record PricingCalculationResult
        (
        decimal FilamentCost,
        decimal EnergyCost,
        decimal DepreciationCost,
        decimal MaintenanceReserveCost,
        decimal BatchCostPerUnit,
        decimal ExtraCostsPerUnit,
        decimal CostPerUnit,
        decimal RetailPrice,
        decimal RetailProfitPerUnit,
        decimal WholesalePrice,
        decimal WholesaleProfitPerUnit,
        decimal? MarketplacePrice
        );
}
