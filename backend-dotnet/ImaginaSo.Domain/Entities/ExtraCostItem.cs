using System;
using System.Collections.Generic;
using System.Text;

namespace ImaginaSo.Domain.Entities
{
    public class ExtraCostItem
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public decimal UnitCost { get; private set; }

        protected ExtraCostItem() { }

        public ExtraCostItem(string name, decimal unitCost)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome do custo extra é obrigatório.", nameof(name));
            if (unitCost <= 0)
                throw new ArgumentException("O valor unitário deve ser maior que zero.", nameof(unitCost));
            Id = Guid.NewGuid();
            Name = name;
            UnitCost = unitCost;
        }

        public void UpdateUnitCost(decimal newUnitCost)
        {
            if (newUnitCost <= 0)
                throw new ArgumentException("O valor unitário deve ser maior que zero.", nameof(newUnitCost));
            UnitCost = newUnitCost;
        }

        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("O nome do custo extra é obrigatório.", nameof(newName));
            Name = newName;
        }

    }
}
