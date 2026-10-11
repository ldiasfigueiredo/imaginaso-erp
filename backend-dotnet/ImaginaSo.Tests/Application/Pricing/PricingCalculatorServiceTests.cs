using ImaginaSo.Application.Pricing;
using ImaginaSo.Domain.Entities;
using Xunit;

namespace ImaginaSo.Tests.Application.Pricing
{
    public class PricingCalculatorServiceTests
    {
        // Tempo da planilha: 4:51:17
        private const decimal SpreadsheetHours = 4m + 51m / 60m + 17m / 3600m;

        private readonly PricingCalculatorService _service = new();
        private readonly Printer _printer = new("Fenix A1", 180m, 6330m, 1m, 8760m);
        private readonly Filament _filament = new("PLA Premium", "3D Lab", "PLA", "Verde", 90m);

        private PricingCalculationInput BuildInput(
            int quantity = 1,
            decimal maintenanceReservePercentage = 0m,
            decimal printingHours = SpreadsheetHours,
            decimal retailMargin = 200m,
            decimal wholesaleMargin = 100m,
            IReadOnlyCollection<ExtraCostItem>? extraCosts = null,
            SalesChannel? channel = null)
        {
            var settings = new GlobalSettings(1.15m, maintenanceReservePercentage);
            extraCosts ??= new[] { new ExtraCostItem("Embalagem", 1.00m) };

            return new PricingCalculationInput(
                _printer, _filament, settings,
                WeightInGrams: 58.62m,
                PrintingHours: printingHours,
                Quantity: quantity,
                ExtraCostItems: extraCosts,
                RetailMarginPercentage: retailMargin,
                WholesaleMarginPercentage: wholesaleMargin,
                SalesChannel: channel);
        }

        // ---------- Cálculo ----------

        [Fact]
        public void Calculate_SpreadsheetScenario_ReturnsExpectedCostAndRetailPrice()
        {
            var result = _service.Calculate(BuildInput());

            Assert.NotNull(result);
            Assert.Equal(5.28m, result.FilamentCost, 2);
            Assert.Equal(1.00m, result.EnergyCost, 2);
            Assert.Equal(3.51m, result.DepreciationCost, 2);
            Assert.Equal(10.79m, result.CostPerUnit, 2);
            Assert.Equal(32.37m, result.RetailPrice, 2);
        }

        [Fact]
        public void Calculate_QuantityTwenty_DividesBatchCostButNotExtraCosts()
        {
            var single = _service.Calculate(BuildInput(quantity: 1));
            var batch = _service.Calculate(BuildInput(quantity: 20));

            Assert.Equal(single.BatchCostPerUnit / 20m, batch.BatchCostPerUnit, 2);
            Assert.Equal(single.ExtraCostsPerUnit, batch.ExtraCostsPerUnit);
        }

        [Fact]
        public void Calculate_MaintenanceReserveTenPercent_IsTenPercentOfDepreciation()
        {
            var result = _service.Calculate(BuildInput(maintenanceReservePercentage: 10m));

            Assert.Equal(result.DepreciationCost * 0.10m, result.MaintenanceReserveCost, 6);
        }

        [Fact]
        public void Calculate_MaintenanceReserveTenPercent_IncreasesCostPerUnit()
        {
            var withoutReserve = _service.Calculate(BuildInput(maintenanceReservePercentage: 0m));
            var withReserve = _service.Calculate(BuildInput(maintenanceReservePercentage: 10m));

            Assert.True(withReserve.CostPerUnit > withoutReserve.CostPerUnit);
        }

        [Fact]
        public void Calculate_WholesaleMarginOneHundredPercent_ReturnsDoubleTheCost()
        {
            // Planilha: 100% de lucro → R$ 21,58 de preço e R$ 10,79 de lucro
            var result = _service.Calculate(BuildInput(wholesaleMargin: 100m));

            Assert.Equal(21.58m, result.WholesalePrice, 2);
            Assert.Equal(10.79m, result.WholesaleProfitPerUnit, 2);
        }

        [Fact]
        public void Calculate_MultipleExtraCosts_SumsAllUnitCosts()
        {
            var extras = new[]
            {
                new ExtraCostItem("Argola de chaveiro", 0.50m),
                new ExtraCostItem("Tag impressa", 0.30m),
                new ExtraCostItem("Saquinho", 0.25m)
            };

            var result = _service.Calculate(BuildInput(extraCosts: extras));

            Assert.Equal(1.05m, result.ExtraCostsPerUnit);
        }

        [Fact]
        public void Calculate_NoExtraCosts_ReturnsZeroExtraCostsPerUnit()
        {
            var result = _service.Calculate(BuildInput(extraCosts: Array.Empty<ExtraCostItem>()));

            Assert.Equal(0m, result.ExtraCostsPerUnit);
        }

        // ---------- Marketplace ----------

        [Fact]
        public void Calculate_WithoutSalesChannel_MarketplacePriceIsNull()
        {
            var result = _service.Calculate(BuildInput(channel: null));

            Assert.Null(result.MarketplacePrice);
        }

        [Fact]
        public void Calculate_WithSalesChannel_MarketplacePriceIsHigherThanRetailPrice()
        {
            var channel = new SalesChannel("Mercado Livre", 14m);

            var result = _service.Calculate(BuildInput(channel: channel));

            Assert.NotNull(result.MarketplacePrice);
            Assert.True(result.MarketplacePrice > result.RetailPrice);
        }

        [Fact]
        public void Calculate_WithSalesChannel_LeavesSameProfitAsRetailAfterFee()
        {
            var channel = new SalesChannel("Mercado Livre", 14m);

            var result = _service.Calculate(BuildInput(channel: channel));

            var netAfterFee = result.MarketplacePrice!.Value * (1 - channel.FeePercentage / 100m);
            var profitAfterFee = netAfterFee - result.CostPerUnit;

            Assert.Equal(result.RetailProfitPerUnit, profitAfterFee, 2);
        }

        // ---------- Validação ----------

        [Fact]
        public void Calculate_NullInput_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.Calculate(null!));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Calculate_InvalidPrintingHours_ThrowsArgumentException(decimal invalidHours)
        {
            Assert.Throws<ArgumentException>(() =>
                _service.Calculate(BuildInput(printingHours: invalidHours)));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Calculate_InvalidQuantity_ThrowsArgumentException(int invalidQuantity)
        {
            Assert.Throws<ArgumentException>(() =>
                _service.Calculate(BuildInput(quantity: invalidQuantity)));
        }

        [Theory]
        [InlineData(-1, 100)]
        [InlineData(200, -1)]
        public void Calculate_NegativeMargin_ThrowsArgumentException(decimal retailMargin, decimal wholesaleMargin)
        {
            Assert.Throws<ArgumentException>(() =>
                _service.Calculate(BuildInput(retailMargin: retailMargin, wholesaleMargin: wholesaleMargin)));
        }
    }
}