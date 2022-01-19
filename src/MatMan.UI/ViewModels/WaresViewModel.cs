using System;
using System.Linq;
using System.Collections.Generic;
using MatMan.Domain.Models;

namespace MatMan.UI.ViewModels
{
    public class WaresViewModel : ConfigurableEntityViewModel<Ware, WareConfiguration>
    {
        public List<Material> Materials { get; init; }

        public List<MaterialConfiguration> MaterialsConfigurations { get; init; }

        public List<WareMaterial> WaresMaterials { get; init; }

        public SelectableEntity<Material>[] SelectableMaterials { get; init; }

        public List<WareMaterial> GetWareMaterials(Guid wareID) =>
            WaresMaterials.Where(wm => wm.WareID == wareID).ToList();

        public string GetMaterialUnitShortName(Guid materialID)
        {
            var configuration = MaterialsConfigurations.FirstOrDefault(mc => mc.ComponentID == materialID);

            if (configuration == null) return "шт";
            else return configuration.Unit.ShortName;
        }
    }
}
