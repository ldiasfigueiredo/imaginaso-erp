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
            var depreciationCost = 0.50m;
            var model = "A1 Combo";
            var manufacturer = "Bambu Lab";
            // Act
            var printer = new Printer(name, powerConsumption, depreciationCost, model, manufacturer);
            // Assert
            Assert.NotEqual(Guid.Empty, printer.Id);
            Assert.Equal(name, printer.Name);
            Assert.Equal(powerConsumption, printer.PowerConsumptionWatts);
            Assert.Equal(depreciationCost, printer.DepreciationCostPerHour);
            Assert.Equal(model, printer.Model);
            Assert.Equal(manufacturer, printer.Manufacturer);
        }

        [Fact]
        public void Constructor_EmptyName_ThrowsArgumentException()
        {
            // Arrange
            var name = "";
            var powerConsumption = 180m;
            var depreciationCost = 0.50m;
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Printer(name, powerConsumption, depreciationCost));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Constructor_InvalidPower_ThrowsArgumentException(decimal powerConsumption)
        {
            // Arrange
            var name = "Fenix A1";
            var depreciationCost = 0.50m;
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Printer(name, powerConsumption, depreciationCost));
        }

        [Fact]
        public void Constructor_NegativeDeprecation_ThrowsArgumentException()
        {
            // Arrange
            var name = "Fenix A1";
            var powerConsumption = 180m;
            var depreciationCost = -0.1m;
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Printer(name, powerConsumption, depreciationCost));
        }
        [Fact]
        public void UpdateDeprecation_ValidValue_UpdatesProperty()
        {
            // Arrange
            var printer = new Printer("Fenix A1", 180m, 0.50m);
            var newDeprecationCost = 0.72m;
            // Act
            printer.UpdateDeprecation(newDeprecationCost);
            // Assert
            Assert.Equal(newDeprecationCost, printer.DepreciationCostPerHour);
        }
        [Fact]
        public void UpdateDeprecation_NegativeValue_ThrowsArgumentException()
        {
            // Arrange
            var printer = new Printer("Fenix A1", 180m, 0.50m);
            var newDeprecationCost = -0.1m;
            // Act & Assert
            Assert.Throws<ArgumentException>(() => printer.UpdateDeprecation(newDeprecationCost));
        }
    }
}
