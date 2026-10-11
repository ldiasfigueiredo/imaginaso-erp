using ImaginaSo.Domain.Entities;

namespace ImaginaSo.Application.Pricing
{
    public record PricingCalculationInput(
        Printer Printer,
        Filament Filament,
        GlobalSettings Settings,
        decimal WeightInGrams,
        decimal PrintingHours,
        int Quantity,
        IReadOnlyCollection<ExtraCostItem> ExtraCostItems,
        decimal RetailMarginPercentage,
        decimal WholesaleMarginPercentage,
        SalesChannel? SalesChannel = null);
}
