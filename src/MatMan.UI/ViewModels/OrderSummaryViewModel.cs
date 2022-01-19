using System;
using System.Linq;
using System.Collections.Generic;
using MatMan.Domain.Models;

namespace MatMan.UI.ViewModels
{
    public class OrderSummaryViewModel
    {
        public Order Order { get; init; }

        public List<OrderComponent<Material>> AllUsedMaterials { get; init; }

        public List<OrderComponent<Ware>> AllUsedWares { get; init; }

        public List<OrderWork> UsedWorks { get; init; }

        public List<MaterialConfiguration> MaterialConfigurations { get; init; }

        public List<WareConfiguration> WaresConfigurations { get; init; }

        public List<OrderPerimeter> OrderPerimeters { get; init; }

        public double CommonPerimeter =>
            OrderPerimeters.Sum(op => op.Perimeter) ?? 0;

        public bool HasWorks => UsedWorks?.Count != 0;

        public bool HasPerimeter =>
            CommonPerimeter != 0;

        public bool HasDescription =>
            Order.Desription != null || Order.Desription?.Length > 0;

        public bool HasPerimeterOrDescritption =>
            HasPerimeter || HasDescription;

        public bool HasWares =>
            AllUsedWares != null && AllUsedWares?.Count > 0;

        public MaterialConfiguration GetMaterialConfiguration(Guid materialID) =>
            MaterialConfigurations.FirstOrDefault(mc => mc.ComponentID == materialID);

        public WareConfiguration GetWareConfiguration(Guid wareID) =>
            WaresConfigurations.FirstOrDefault(mc => mc.ComponentID == wareID);
    }
}
