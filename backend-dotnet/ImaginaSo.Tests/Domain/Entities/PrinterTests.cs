using ImaginaSo.Domain.Entities;
using Xunit;

namespace ImaginaSo.Tests.Domain.Entities
{
    public class PrinterTests
    {
        [Fact]
        public void Constructor_ValidData_CreatesPrinterWithCorrectProperties()
        {
            // Arrange
            var name = "Fenix A1";
            var powerConsumption = 180m;
            var purchasePrice = 6330m;
            var investmentReturnYears = 1m;
            var hoursWorkedPerYear = 8760m;
            var model = "A1 Combo";
            var manufacturer = "Bambu Lab";

            // Act
            var printer = new Printer(name, powerConsumption, purchasePrice, investmentReturnYears, hoursWorkedPerYear, model, manufacturer);

            // Assert
            Assert.NotEqual(Guid.Empty, printer.Id);
            Assert.Equal(name, printer.Name);
            Assert.Equal(powerConsumption, printer.PowerConsumptionWatts);
            Assert.Equal(purchasePrice, printer.PurchasePrice);
            Assert.Equal(investmentReturnYears, printer.InvestmentReturnYears);
            Assert.Equal(hoursWorkedPerYear, printer.HoursWorkedPerYear);
            Assert.Equal(model, printer.Model);
            Assert.Equal(manufacturer, printer.Manufacturer);
        }

        [Fact]
        public void Constructor_ValidData_CalculatesDepreciationCostPerHourCorrectly()
        {
            // Regra: valor da máquina ÷ (anos de retorno × horas trabalhadas/ano)
            // 6330 / (1 * 8760) = 0.7226027397...
            var printer = new Printer("Fenix A1", 180m, 6330m, 1m, 8760m);

            Assert.Equal(0.7226027397260273972602739726m, printer.DepreciationCostPerHour);
        }

        [Fact]
        public void Constructor_EmptyName_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Printer("", 180m, 6330m, 1m, 8760m));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Constructor_InvalidPower_ThrowsArgumentException(decimal powerConsumption)
        {
            Assert.Throws<ArgumentException>(() =>
                new Printer("Fenix A1", powerConsumption, 6330m, 1m, 8760m));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Constructor_InvalidPurchasePrice_ThrowsArgumentException(decimal purchasePrice)
        {
            Assert.Throws<ArgumentException>(() =>
                new Printer("Fenix A1", 180m, purchasePrice, 1m, 8760m));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Constructor_InvalidInvestmentReturnYears_ThrowsArgumentException(decimal investmentReturnYears)
        {
            Assert.Throws<ArgumentException>(() =>
                new Printer("Fenix A1", 180m, 6330m, investmentReturnYears, 8760m));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Constructor_InvalidHoursWorkedPerYear_ThrowsArgumentException(decimal hoursWorkedPerYear)
        {
            Assert.Throws<ArgumentException>(() =>
                new Printer("Fenix A1", 180m, 6330m, 1m, hoursWorkedPerYear));
        }

        [Fact]
        public void UpdateDepreciationInputs_ValidValues_RecalculatesDepreciationCostPerHour()
        {
            var printer = new Printer("Fenix A1", 180m, 6330m, 1m, 8760m);

            printer.UpdateDepreciationInputs(8000m, 2m, 8760m);

            // 8000 / (2 * 8760) = 0.4566210045...
            Assert.Equal(8000m, printer.PurchasePrice);
            Assert.Equal(2m, printer.InvestmentReturnYears);
            Assert.Equal(0.4566210045662100456621004566m, printer.DepreciationCostPerHour);
        }

        [Fact]
        public void UpdateDepreciationInputs_InvalidPurchasePrice_ThrowsArgumentException()
        {
            var printer = new Printer("Fenix A1", 180m, 6330m, 1m, 8760m);

            Assert.Throws<ArgumentException>(() =>
                printer.UpdateDepreciationInputs(0m, 1m, 8760m));
        }
    }
}