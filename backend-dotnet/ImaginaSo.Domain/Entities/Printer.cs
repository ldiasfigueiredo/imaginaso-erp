using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace ImaginaSo.Domain.Entities
{
    public class Printer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Model { get; private set; }
        public string? Manufacturer { get; private set; }
        public decimal PowerConsumptionWatts { get; private set; }
        public decimal PurchasePrice { get; private set; }
        public decimal InvestmentReturnYears { get; private set; }
        public decimal HoursWorkedPerYear { get; private set; }
        public decimal DepreciationCostPerHour { get; private set; }

        protected Printer() { }

        public Printer(
            string name,
            decimal powerConsumptionWatts,
            decimal purchasePrice,
            decimal investmentReturnYears,
            decimal hoursWorkedPerYear,
            string? model = null,
            string? manufacturer = null
            )
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome da impressora é obrigatório.", nameof(name));

            if (powerConsumptionWatts <= 0)
                throw new ArgumentException("A potência deve ser maior que zero.", nameof(powerConsumptionWatts));

            if (purchasePrice <= 0)
                throw new ArgumentException("O valor da máquina deve ser maior que zero.", nameof(purchasePrice));

            if (investmentReturnYears <= 0)
                throw new ArgumentException("O tempo de retorno do investimento deve ser maior que zero.", nameof(investmentReturnYears));

            if (hoursWorkedPerYear <= 0)
                throw new ArgumentException("As horas trabalhadas por ano devem ser maiores que zero.", nameof(hoursWorkedPerYear));

            Id = Guid.NewGuid();
            Name = name;
            Model = model;
            Manufacturer = manufacturer;
            PowerConsumptionWatts = powerConsumptionWatts;
            PurchasePrice = purchasePrice;
            InvestmentReturnYears = investmentReturnYears;
            HoursWorkedPerYear = hoursWorkedPerYear;
            DepreciationCostPerHour = CalculateDepreciationPerHour(purchasePrice, investmentReturnYears, hoursWorkedPerYear);
        }

        private static decimal CalculateDepreciationPerHour(
        decimal purchasePrice, decimal investmentReturnYears, decimal hoursWorkedPerYear)
        => purchasePrice / (investmentReturnYears * hoursWorkedPerYear);

        public void UpdateDepreciationInputs(
            decimal newPurchasePrice, decimal newInvestmentReturnYears, decimal newHoursWorkedPerYear)
        {
            if (newPurchasePrice <= 0)
                throw new ArgumentException("O valor da máquina deve ser maior que zero.", nameof(newPurchasePrice));

            if (newInvestmentReturnYears <= 0)
                throw new ArgumentException("O tempo de retorno do investimento deve ser maior que zero.", nameof(newInvestmentReturnYears));

            if (newHoursWorkedPerYear <= 0)
                throw new ArgumentException("As horas trabalhadas por ano devem ser maiores que zero.", nameof(newHoursWorkedPerYear));

            PurchasePrice = newPurchasePrice;
            InvestmentReturnYears = newInvestmentReturnYears;
            HoursWorkedPerYear = newHoursWorkedPerYear;
            DepreciationCostPerHour = CalculateDepreciationPerHour(newPurchasePrice, newInvestmentReturnYears, newHoursWorkedPerYear);
        }
    }
}

