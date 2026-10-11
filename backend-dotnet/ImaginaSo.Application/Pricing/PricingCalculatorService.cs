using System.Net.Http.Headers;

namespace ImaginaSo.Application.Pricing
{
    public class PricingCalculatorService: IPricingCalculatorService
    {
        public PricingCalculationResult Calculate(PricingCalculationInput input)
        {
            ArgumentNullException.ThrowIfNull(input);
            Validate(input);

            // Custos do lote inteiro (a placa toda)
            var filamentCost = input.Filament.CalculateCostForGrams(input.WeightInGrams);

            var energyCost = (input.Printer.PowerConsumptionWatts / 1000m) * input.PrintingHours * input.Settings.ElectricityCostPerKWh;

            var depreciationCost = input.Printer.DepreciationCostPerHour * input.PrintingHours;

            var maintenanceReserveCost = depreciationCost * (input.Settings.MaintenanceReservePercentage / 100m);

            var batchCostPerUnit = (filamentCost + energyCost + depreciationCost + maintenanceReserveCost) / input.Quantity;

            // Custos que já nascem por peça (argola, tag, saquinho...)
            var extraCostsPerUnit = input.ExtraCostItems.Sum(item => item.UnitCost);

            var costPerUnit = batchCostPerUnit + extraCostsPerUnit;

            // Preços
            var retailPrice = costPerUnit * (1 + input.RetailMarginPercentage / 100m);
            var wholesalePrice = costPerUnit * (1 + input.WholesaleMarginPercentage / 100m);

            //Marketplace: mantém o mesmo lucro do varejo, já descontando a taxa do canal
            var retailProfit = retailPrice - costPerUnit;
            var marketplacePrice = input.SalesChannel?.CalculatePriceForDesiredProfit(costPerUnit, retailProfit);

            return new PricingCalculationResult(
                filamentCost,
                energyCost,
                depreciationCost,
                maintenanceReserveCost,
                batchCostPerUnit,
                extraCostsPerUnit,
                costPerUnit,
                retailPrice,
                retailProfit,
                wholesalePrice,
                wholesalePrice - costPerUnit,
                marketplacePrice
                );
        }

        private static void Validate(PricingCalculationInput input)
        {
            ArgumentNullException.ThrowIfNull(input.Printer);
            ArgumentNullException.ThrowIfNull(input.Filament);
            ArgumentNullException.ThrowIfNull(input.Settings);
            ArgumentNullException.ThrowIfNull(input.ExtraCostItems);

            if (input.WeightInGrams <= 0)
                throw new ArgumentException("O peso em gramas deve ser maior que zero.", nameof(input.WeightInGrams));
            if (input.PrintingHours <= 0)
                throw new ArgumentException("As horas de impressão devem ser maiores que zero.", nameof(input.PrintingHours));
            if (input.Quantity <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(input.Quantity));
            if (input.RetailMarginPercentage < 0 || input.WholesaleMarginPercentage < 0)
                throw new ArgumentException("A margem de lucro não pode ser negativa.");
        }
    }
}
