using System;
using System.Collections.Generic;
using System.Linq;
using MatMan.Data;
using MatMan.Domain.Models;

namespace MatMan.Application.Providers
{
    public class MaterialsProvider : ComponentsProvider<Material, MaterialConfiguration>
    {
        public MaterialsProvider(IRepository repo) : base(repo) { }

        public IEnumerable<Ware> GetWaresMaterialUsedIn(Guid materialID) =>
            _repository.Get<WareMaterial>(
                filter: wm => wm.MaterialID == materialID,
                includeProperties: "Ware"
            ).Select(wm => wm.Ware);

        public IEnumerable<Work> GetWorksMaterialUsedIn(Guid materialID) =>
            _repository.Get<WorkMaterial>(
                filter: wm => wm.MaterialID == materialID,
                includeProperties: "Work"
            ).Select(wm => wm.Work).Distinct();

        public bool IsUsedInAnyWares(Guid materialID) =>
            GetWaresMaterialUsedIn(materialID).Count() != 0;

        public bool IsUsedInAnyWorks(Guid materialID) =>
            GetWorksMaterialUsedIn(materialID).Count() != 0;
    }
}
