using System;
using System.Linq;
using System.Collections.Generic;
using MatMan.Data;
using MatMan.Domain.Models;

namespace MatMan.Application.Providers
{
    public class OrdersProvider : UnitOfWorkBase, IOrdersProvider
    {
        public OrdersProvider(IRepository repo) : base(repo) { }

        public IEnumerable<Order> AllOrders =>
            _repository.Get<Order>();

        public Order GetOrderBy(Guid orderID) =>
            _repository.GetByPrimaryKey<Order>(orderID);

        public IEnumerable<OrderPerimeter> GetOrderPerimeters(Guid orderID) =>
            _repository.Get<OrderPerimeter>(
                filter: op => op.OrderID == orderID,
                includeProperties: "PerimeterType"
            );

        public IEnumerable<PerimeterType> AllPerimeterTypes =>
            _repository.Get<PerimeterType>();

        public IEnumerable<OrderComponent<TComponent>> GetOrderComponents<TComponent>(Guid orderID)
            where TComponent : Component
        {
            if (DoesOrderExist(orderID))
                throw new EntityDoesNotExistException(
                    message: "Невозможно получить компоненты, использованные в проекте" +
                        ", которого не существует",
                    entityName: $"{nameof(Order)}"
                );

            return _repository.Get<OrderComponent<TComponent>>(
                filter: oc => oc.OrderID == orderID,
                includeProperties: "Component"
            );
        }

        public IEnumerable<OrderWork> GetOrderWorks(Guid orderID)
        {
            if (DoesOrderExist(orderID))
                throw new EntityDoesNotExistException(
                    message: "Невозможно получить работы, использованные в проекте" +
                        ", которого не существует",
                    entityName: $"{nameof(Order)}"
                );

            var uniqWorksIDs = _repository.Get<OrderWork>().
                Select(ow => ow.WorkID).Distinct();

            var result = new List<OrderWork>(uniqWorksIDs.Count());

            foreach (var id in uniqWorksIDs)
            {
                result.Add(
                    new OrderWork {
                        Work = _repository.GetByPrimaryKey<Work>(id),
                        WorkID = id,
                        Perimeter = _repository.Get<OrderWork>().
                            Where(ow => ow.WorkID == id && ow.OrderID == orderID).
                                Sum(ow => ow.Perimeter),
                        Order = _repository.GetByPrimaryKey<Order>(orderID),
                        OrderID = orderID
                    }
                );
            }

            return result;
        }

        private bool DoesOrderExist(Guid orderID) =>
            GetOrderBy(orderID) == null;
    }
}
