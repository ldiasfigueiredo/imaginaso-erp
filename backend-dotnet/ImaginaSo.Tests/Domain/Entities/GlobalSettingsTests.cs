using ImaginaSo.Domain.Entities;
using Xunit;

namespace ImaginaSo.Tests.Domain.Entities
{
    public class GlobalSettingsTests
    {
        [Fact]
        public void Constructor_ValidData_CreatesGlobalSettingsWithCorrectValues()
        {
            // Arrange
            var electricityCost = 0.50m;
            var maintenanceReserve = 10m;
            // Act
            var settings = new GlobalSettings(electricityCost, maintenanceReserve);
            // Assert
            Assert.NotEqual(Guid.Empty, settings.Id);
            Assert.Equal(electricityCost, settings.ElectricityCostPerKWh);
            Assert.Equal(maintenanceReserve, settings.MaintenanceReservePercentage);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Constructor_InvalidElectricityCost_ThrowsArgumentException(decimal invalidCost)
        {
            // Arrange
            var maintenanceReserve = 10m;
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new GlobalSettings(invalidCost, maintenanceReserve));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(101)]
        public void Constructor_InvalidMaintenancePercentage_ThrowsArgumentException(decimal invalidPercentage)
        {
            // Arrange
            var electricityCost = 0.50m;
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new GlobalSettings(electricityCost, invalidPercentage));
        }

        [Fact]
        public void UpdateElectricityCost_ValidValue_UpdatesProperty()
        {
            // Arrange
            var settings = new GlobalSettings(0.50m, 10m);
            var newCost = 0.60m;
            // Act
            settings.UpdateElectricityCost(newCost);
            // Assert
            Assert.Equal(newCost, settings.ElectricityCostPerKWh);
        }

        [Fact]
        public void UpdateElectricityCost_ZeroOrNegative_ThrowsArgumentException()
        {
            // Arrange
            var settings = new GlobalSettings(0.50m, 10m);
            // Act & Assert
            Assert.Throws<ArgumentException>(() => settings.UpdateElectricityCost(0));
            Assert.Throws<ArgumentException>(() => settings.UpdateElectricityCost(-0.1m));
        }
    }
}
