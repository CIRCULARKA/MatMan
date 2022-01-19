using System;
using System.Linq;
using System.Collections.Generic;
using MatMan.Domain.Models;

namespace MatMan.UI.ViewModels
{
    public class CreateOrderViewModel
    {
        public Order NewOrder { get; init; }

        public List<Ware> Wares { get; init; }

        public List<Work> Works { get; init; }

        public List<PerimeterType> PerimeterTypes { get; init; }

        public List<WareConfiguration> WaresConfigurations { get; init; }

        public SelectableEntity<Ware>[] SelectableWares { get; init; }

        public SelectableEntity<Work>[] SelectableWorks { get; init; }

        public OrderPerimeter[] NewOrderPerimeters { get; init; }

        public string GetWareUnitShortName(Guid wareID)
        {
            var config = WaresConfigurations.FirstOrDefault(wc => wc.ComponentID == wareID);

            if (config == null) return "шт";
            else return config.Unit.ShortName;
        }
    }
}
