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
        public decimal DepreciationCostPerHour { get; private set; }

        protected Printer() { }

        public Printer(
            string name,
            decimal powerConsumptionWatts,
            decimal depreciationCostPerHour,
            string? model = null,
            string? manufacturer = null
            )
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome da impressora é obrigatório.", nameof(name));

            if (powerConsumptionWatts <= 0)
                throw new ArgumentException("A potência deve ser maior que zero.", nameof(powerConsumptionWatts));

            if (depreciationCostPerHour < 0)
                throw new ArgumentException("O custo de depreciação não pode ser negativo.", nameof(depreciationCostPerHour));
            
            Id = Guid.NewGuid();
            Name = name;
            Model = model;
            Manufacturer = manufacturer;
            PowerConsumptionWatts = powerConsumptionWatts;
            DepreciationCostPerHour = depreciationCostPerHour;
        }

        public void UpdateDeprecation(decimal newDepreciationCostPerHour)
        {
            if (newDepreciationCostPerHour < 0)
                throw new ArgumentException("O custo de depreciação não pode ser negativo.", nameof(newDepreciationCostPerHour));
            DepreciationCostPerHour = newDepreciationCostPerHour;
        }
    }
}

