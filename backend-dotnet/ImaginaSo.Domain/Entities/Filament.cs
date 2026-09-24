using System;
using System.Collections.Generic;
using System.Text;

namespace ImaginaSo.Domain.Entities
{
    public class Filament
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string Manufacturer { get; private set; } = null!;
        public string MaterialType { get; private set; } = null!;
        public string Color { get; private set; } = null!;
        public string? ColorHex { get; private set; }
        public decimal PricePerKg { get; private set; }

        protected Filament() { }

        public Filament(
            string name,
            string manufacturer,
            string materialType,
            string color,
            decimal pricePerKg,
            string? colorHex = null
        )
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome do filamento é obrigatório.", nameof(name));
            if (string.IsNullOrWhiteSpace(manufacturer))
                throw new ArgumentException("O fabricante é obrigatório.", nameof(manufacturer));
            if (string.IsNullOrWhiteSpace(materialType))
                throw new ArgumentException("O tipo de material é obrigatório.", nameof(materialType));
            if (pricePerKg <= 0)
                throw new ArgumentException("O preço por kg deve ser maior que zero.", nameof(pricePerKg));
            if (string.IsNullOrWhiteSpace(color))
                throw new ArgumentException("A cor do filamento é obrigatória.", nameof(color));
            if (pricePerKg <= 0)
                throw new ArgumentException("O preço por kg deve ser maior que zero.", nameof(pricePerKg));
            if (colorHex is not null && !System.Text.RegularExpressions.Regex.IsMatch(colorHex, "^#[0-9A-Fa-f]{6}$"))
                throw new ArgumentException("O código hexadecimal da cor é inválido (formato esperado: #RRGGBB).", nameof(colorHex));

            Id = Guid.NewGuid();
            Name = name;
            Manufacturer = manufacturer;
            MaterialType = materialType;
            Color = color;
            PricePerKg = pricePerKg;
            ColorHex = colorHex;
        }

        public decimal CalculateCostForGrams(decimal weightInGrams)
        {
            if (weightInGrams <= 0)
                throw new ArgumentException("O peso deve ser maior que zero.", nameof(weightInGrams));
            return (weightInGrams / 1000) * PricePerKg;
        }
    }
}
