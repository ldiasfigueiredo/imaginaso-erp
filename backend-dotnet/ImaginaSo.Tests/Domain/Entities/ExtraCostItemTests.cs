using ImaginaSo.Domain.Entities;
using Xunit;

namespace ImaginaSo.Tests.Domain.Entities
{
    public class ExtraCostItemTests
    {
        [Fact]
        public void Constructor_ValidData_CreatesExtraCostItemWithCorrectValues()
        {
            // Arrange
            var name = "Argola de chaveiro";
            var unitCost = 0.50m;

            // Act
            var item = new ExtraCostItem(name, unitCost);

            // Assert
            Assert.NotEqual(Guid.Empty, item.Id);
            Assert.Equal(name, item.Name);
            Assert.Equal(unitCost, item.UnitCost);
        }

        [Fact]
        public void Constructor_EmptyName_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new ExtraCostItem("", 0.50m));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Constructor_InvalidUnitCost_ThrowsArgumentException(decimal invalidUnitCost) 
        {
            Assert.Throws<ArgumentException>(() =>
                new ExtraCostItem("Argola de chaveiro", invalidUnitCost));
        }

        [Fact]
        public void UpdateUnitCost_ValidValue_UpdatesProperty()
        {
            // Arrange
            var item = new ExtraCostItem("Argola de chaveiro", 0.50m);

            // Act
            item.UpdateUnitCost(0.65m);

            // Assert
            Assert.Equal(0.65m, item.UnitCost);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void UpdateUnitCost_InvalidValue_ThrowsArgumentException(decimal invalidUnitCost)
        {
            // Arrange
            var item = new ExtraCostItem("Argola de chaveiro", 0.50m);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => item.UpdateUnitCost(invalidUnitCost));
        }

        [Fact]
        public void Rename_ValidName_UpdatesProperty()
        {
            // Arrange
            var item = new ExtraCostItem("Argola de chaveiro", 0.50m);

            // Act
            item.Rename("Argola de chaveiro reforçada");

            // Assert
            Assert.Equal("Argola de chaveiro reforçada", item.Name);
        }

        [Fact]
        public void Rename_EmptyName_ThrowsArgumentException()
        {
            // Arrange
            var item = new ExtraCostItem("Argola de chaveiro", 0.50m);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => item.Rename(""));
        }
    }
}
