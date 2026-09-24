using System;
using System.Collections.Generic;
using System.Text;

namespace ImaginaSo.Domain.Entities
{
    public class GlobalSettings
    {
        public Guid Id { get; private set; }
        public decimal ElectricityCostPerKWh { get; private set; }
        public decimal MaintenanceReservePercentage { get; private set; }

        protected GlobalSettings() { }

        public GlobalSettings(decimal electricityCostPerKWh, decimal maintenanceReservePercentage)
        {
            if (electricityCostPerKWh <= 0)
                throw new ArgumentException("O valor do kWh deve ser maior que zero.", nameof(electricityCostPerKWh));
            if (maintenanceReservePercentage < 0 || maintenanceReservePercentage > 100)
                throw new ArgumentException("O percentual de reserva deve estar entre 0 e 100.", nameof(maintenanceReservePercentage));

            Id = Guid.NewGuid();
            ElectricityCostPerKWh = electricityCostPerKWh;
            MaintenanceReservePercentage = maintenanceReservePercentage;
        }

        public void UpdateElectricityCost(decimal newCost)
        {
            if (newCost <= 0)
                throw new ArgumentException("O valor do kWh deve ser maior que zero.", nameof(newCost));
            ElectricityCostPerKWh = newCost;
        }
    }
}
