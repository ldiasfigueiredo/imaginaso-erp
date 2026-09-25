using ImaginaSo.Domain.Entities;
using Xunit;

namespace ImaginaSo.Tests.Domain.Entities
{
    public class SalesChannelTests
    {
        [Fact]
        public void Constructor_ValidData_CreatesSalesChannelWithCorrectValues()
        {
            // Arrange
            var name = "Mercado Livre";
            var feePercentage = 14m;
            // Act
            var channel = new SalesChannel(name, feePercentage);
            // Assert
            Assert.NotEqual(Guid.Empty, channel.Id);
            Assert.Equal(name, channel.Name);
            Assert.Equal(feePercentage, channel.FeePercentage);
        }
        [Fact]
        public void Constructor_EmptyName_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new SalesChannel("", 14m));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(100)]
        [InlineData(150)]
        public void Constructor_InvalidFeePercentage_ThrowsArgumentException(decimal invalidFee)
        {
            Assert.Throws<ArgumentException>(() =>
                new SalesChannel("Mercado Livre", invalidFee));
        }

        [Fact]
        public void Constructor_ZeroFeePercentage_IsValid()
        {
            var channel = new SalesChannel("Venda Direta", 0m);
            Assert.Equal(0m, channel.FeePercentage);
        }

        [Fact]
        public void UpdateFeePercentage_ValidValue_UpdatesProperty()
        {
            // Arrange
            var channel = new SalesChannel("Mercado Livre", 14m);
            // Act
            channel.UpdateFeePercentage(16m);
            // Assert
            Assert.Equal(16m, channel.FeePercentage);
        }

        [Fact]
        public void CalculatePriceForDesiredProfit_ReturnsCorrectPrice()
        {
            // custo R$10,79 + lucro desejado R$15,00, taxa 14%
            // preço = (10.79 + 15) / (1 - 0.14) = 25.79 / 0.86 = 29.988372...
            var channel = new SalesChannel("Mercado Livre", 14m);
            var price = channel.CalculatePriceForDesiredProfit(10.79m, 15m);
            Assert.Equal(29.99m, Math.Round(price, 2));
        }

        [Fact]
        public void CalculatePriceForDesiredProfit_AfterFeeDeduction_LeavesExactDesiredProfit()
        {
            var channel = new SalesChannel("Shopee", 20m);
            var cost = 10.79m;
            var desiredProfit = 15m;
            var price = channel.CalculatePriceForDesiredProfit(cost, desiredProfit);
            var profitAfterFee = price * (1 - channel.FeePercentage / 100);
            var actualProfit = profitAfterFee - cost;
            Assert.Equal(desiredProfit, Math.Round(actualProfit, 2));
        }
    }
}
