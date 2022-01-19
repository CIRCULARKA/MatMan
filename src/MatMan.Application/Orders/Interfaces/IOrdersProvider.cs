using System;
using System.Collections.Generic;
using MatMan.Domain.Models;

namespace MatMan.Application.Providers
{
    public interface IOrdersProvider
    {
        Order GetOrderBy(Guid orderID);

        IEnumerable<Order> AllOrders { get; }

        IEnumerable<OrderPerimeter> GetOrderPerimeters(Guid orderID);

        IEnumerable<PerimeterType> AllPerimeterTypes { get; }

        IEnumerable<OrderComponent<T>> GetOrderComponents<T>(Guid orderID)
            where T : Component;

        IEnumerable<OrderWork> GetOrderWorks(Guid orderID);
    }
}
