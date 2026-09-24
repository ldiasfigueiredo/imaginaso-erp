using ImaginaSo.Domain.Entities;
using Xunit;

namespace ImaginaSo.Tests.Domain.Entities
{
    public class FilamentTests
    {
        [Fact]
        public void Constructor_ValidData_CreatesFilamentWithCorrectValues()
        {
            // Arrange
            var name = "PLA Premium";
            var manufacturer = "3D Lab";
            var materialType = "PLA";
            var color = "Verde Arvore";
            var pricePerKg = 90m;
            var colorHex = "#113532";
            // Act
            var filament = new Filament(name, manufacturer, materialType, color, pricePerKg, colorHex);
            // Assert
            Assert.NotEqual(Guid.Empty, filament.Id);
            Assert.Equal(name, filament.Name);
            Assert.Equal(manufacturer, filament.Manufacturer);
            Assert.Equal(materialType, filament.MaterialType);
            Assert.Equal(color, filament.Color);
            Assert.Equal(pricePerKg, filament.PricePerKg);
            Assert.Equal(colorHex, filament.ColorHex);
        }

        [Fact]
        public void Constructor_WithoutColorHex_CreatesFilamentWithNullColorHex()
        {
            // Arrange
            var name = "PLA Premium";
            var manufacturer = "3D Lab";
            var materialType = "PLA";
            var color = "Verde Arvore";
            var pricePerKg = 90m;
            // Act
            var filament = new Filament(name, manufacturer, materialType, color, pricePerKg);
            // Assert
            Assert.Null(filament.ColorHex);
        }

        [Theory]
        [InlineData("#GGGGGG")]
        [InlineData("E53935")]
        [InlineData("#FFF")]
        public void Constructor_InvalidColorHex_ThrowsArgumentException(string invalidColorHex)
        {
            // Arrange
            var name = "PLA Premium";
            var manufacturer = "3D Lab";
            var materialType = "PLA";
            var color = "Verde Arvore";
            var pricePerKg = 90m;
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Filament(name, manufacturer, materialType, color, pricePerKg, invalidColorHex));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Constructor_InvalidPrice_ThrowsArgumentException(decimal invalidPrice)
        {
            // Arrange
            var name = "PLA Premium";
            var manufacturer = "3D Lab";
            var materialType = "PLA";
            var color = "Verde Arvore";
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Filament(name, manufacturer, materialType, color, invalidPrice));
        }

        [Fact]
        public void CalculateCostForGrams_ValidWeight_ReturnsCorrectCost()
        {
            // Arrange
            var filament = new Filament("PLA Premium", "3D Lab", "PLA", "Verde Arvore", 90m);
            var weightInGrams = 500m; // 0.5 kg
            // Regra: (peso em gramas / 1000) * preço por kg
            var expectedCost = (weightInGrams / 1000) * filament.PricePerKg; // 45.0
            // Act
            var actualCost = filament.CalculateCostForGrams(weightInGrams);
            // Assert
            Assert.Equal(expectedCost, actualCost);
        }

        [Fact]
        public void CalculateCostForGrams_ZeroWeight_ThrowsArgumentException()
        {
            // Arrange
            var filament = new Filament("PLA Premium", "3D Lab", "PLA", "Verde Arvore", 90m);
            var weightInGrams = 0m;
            // Act & Assert
            Assert.Throws<ArgumentException>(() => filament.CalculateCostForGrams(weightInGrams));
        }
    }
}
