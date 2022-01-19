using System;
using System.Collections.Generic;
using System.Linq;
using MatMan.Data;
using MatMan.Domain.Models;

namespace MatMan.Application.Providers
{
    public class WaresProvider : ComponentsProvider<Ware, WareConfiguration>
    {
        public WaresProvider(IRepository repo) : base(repo) { }

        public IEnumerable<WareMaterial> GetMaterials() =>
            _repository.Get<WareMaterial>(includeProperties: "Material");

        public IEnumerable<Ware> GetWorksWareUsedIn(Guid wareID) =>
            _repository.Get<WorkWare>(
                filter: wm => wm.WareID == wareID,
                includeProperties: "Ware"
            ).Select(wm => wm.Ware).Distinct();

        public IEnumerable<Order> GetOrdersWareUsedIn(Guid wareID) =>
            _repository.Get<OrderComponent<Ware>>(
                filter: oc => oc.ComponentID == wareID,
                includeProperties: "Order"
            ).Select(oc => oc.Order);
    }
}
