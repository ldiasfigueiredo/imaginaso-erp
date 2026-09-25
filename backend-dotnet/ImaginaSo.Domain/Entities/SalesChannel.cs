using System;
using System.Collections.Generic;
using System.Text;

namespace ImaginaSo.Domain.Entities
{
    public class SalesChannel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public decimal FeePercentage { get; private set; }

        protected SalesChannel() { }

        public SalesChannel(string name, decimal feePercentage)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome do canal de venda é obrigatório.", nameof(name));
            if (feePercentage < 0 || feePercentage >= 100)
                throw new ArgumentException("O percentual de taxa deve estar entre 0 e 100.", nameof(feePercentage));
            Id = Guid.NewGuid();
            Name = name;
            FeePercentage = feePercentage;
        }

        public void UpdateFeePercentage(decimal newFeePercentage)
        {
            if (newFeePercentage < 0 || newFeePercentage >= 100)
                throw new ArgumentException("O percentual de taxa deve estar entre 0 e 100.", nameof(newFeePercentage));
            FeePercentage = newFeePercentage;
        }

        public decimal CalculatePriceForDesiredProfit(decimal costPerUnit, decimal desiredProfit)
        {
            // preço = (custo + lucro desejado) / (1 - taxa / 100)
            // garante que, após o desconto da taxa, sobra exatamente o lucro desejado
            return (costPerUnit + desiredProfit) / (1 - FeePercentage / 100);
        }
    }
}
