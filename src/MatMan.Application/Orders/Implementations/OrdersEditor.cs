using System;
using System.Linq;
using System.Collections.Generic;
using MatMan.Data;
using MatMan.Domain.Models;

namespace MatMan.Application.Editors
{
    public class OrdersEditor : IOrdersEditor
    {
        private IRepository _repository;

        public OrdersEditor(IRepository repo)
        {
            _repository = repo;
        }

        public void CreateOrder(Order newOrder, IEnumerable<OrderPerimeter> orderPerimeters)
        {
            ValidateOrder(newOrder);

            _repository.Add(newOrder);

            foreach (var orderPerimeter in orderPerimeters)
            {
                if ((orderPerimeter.Perimeter ?? 0) == 0) continue;

                var copy = new OrderPerimeter {
                    OrderID = newOrder.ID,
                    PerimeterTypeID = orderPerimeter.PerimeterTypeID,
                    Perimeter = orderPerimeter.Perimeter
                };

                _repository.Add(copy);
            }

            _repository.Save();
        }

        public void DeleteOrder(Guid orderID)
        {
            _repository.Remove<Order>(orderID);
            _repository.Save();
        }

        /// <summary>
        /// Method takes no effect if specified order has no specified perimeter type
        /// </summary>
        public void AddWork(Guid orderID, Guid workID, Guid perimeterTypeID, double workPerimeter)
        {
            ValidatePerimeterType(perimeterTypeID);
            ValidateWorkPerimeter(orderID, workPerimeter);

            if (_repository.GetFirst<OrderPerimeter>(op => op.OrderID == orderID && op.PerimeterTypeID == perimeterTypeID) == null)
                return;

            var targetWork = GetWorkFrom(workID);
            var targetOrder = GetOrderFrom(orderID);

            var correctedPerimeter = targetWork.CalculatePerimeter(workPerimeter);

            _repository.Add(new OrderWork {
                OrderID = targetOrder.ID,
                WorkID = targetWork.ID,
                Perimeter = correctedPerimeter
            });

            var workWares = _repository.Get<WorkWare>(
                filter: ww => ww.WorkID == workID && ww.PerimeterTypeID == perimeterTypeID
            );

            foreach (var workWare in workWares)
                AddComponent<Ware>(
                    orderID,
                    workWare.WareID,
                    CalculateComponentAmount(
                        workWare.WaresAmount,
                        correctedPerimeter,
                        targetWork.ApplicableLength
                    )
                );

            var workMaterials = _repository.Get<WorkMaterial>(
                filter: wm => wm.WorkID == workID && wm.PerimeterTypeID == perimeterTypeID
            );

            foreach (var workMaterial in workMaterials)
                AddMaterial(
                    orderID,
                    workMaterial.MaterialID,
                    CalculateComponentAmount(
                        workMaterial.MaterialsAmount,
                        correctedPerimeter,
                        targetWork.ApplicableLength
                    )
                );

            _repository.Save();
        }

        public void AddMaterial(Guid orderID, Guid materialID, double materialsAmount) =>
            AddComponent<Material>(orderID, materialID, materialsAmount);

        public void AddWare(Guid orderID, Guid wareID, int waresAmount)
        {
            AddComponent<Ware>(orderID, wareID, waresAmount);

            var wareMaterials = _repository.Get<WareMaterial>(
                wm => wm.WareID == wareID
            );

            if (wareMaterials == null)
                throw new EntityDoesNotExistException("Невозможно добавить изделие: изделие не имеет материалов");

            foreach (var wm in wareMaterials)
                AddComponent<Material>(orderID, wm.MaterialID, wm.MaterialsAmount * waresAmount);
        }

        private double CalculateComponentAmount(double components, double workPerimeter, double workApplicableLength) =>
            components * Math.Ceiling(workPerimeter / workApplicableLength);

        private void AddComponent<TComponent>(Guid orderID, Guid componentID, double componentAmount)
            where TComponent : Component
        {
            var targetWare = GetComponentFrom<TComponent>(componentID);
            var targetOrder = GetOrderFrom(orderID);

            if (componentAmount < 1)
                throw new Exception($"Компонент \"{targetWare.Name}\" должен быть в количестве от 1 шт.");

            var existingOrderComponent = _repository.GetFirst<OrderComponent<TComponent>>(
                ow => ow.ComponentID == componentID && ow.OrderID == orderID
            );

            if (existingOrderComponent == null)
            {
                _repository.Add(new OrderComponent<TComponent> {
                    OrderID = orderID,
                    ComponentID = componentID,
                    ComponentAmount = componentAmount
                });
                _repository.Save();
            }
            else
            {
                var existingOrderWareCopy = new OrderComponent<TComponent> {
                    ID = existingOrderComponent.ID,
                    OrderID = orderID,
                    ComponentID = componentID,
                    ComponentAmount = existingOrderComponent.ComponentAmount + componentAmount
                };

                _repository.Remove<OrderComponent<TComponent>>(existingOrderComponent.ID);
                _repository.Save();
                _repository.Add(existingOrderWareCopy);
                _repository.Save();
            }
        }

        private Order GetOrderFrom(Guid orderID)
        {
            var result = _repository.GetByPrimaryKey<Order>(orderID);
            if (result == null)
                throw new EntityDoesNotExistException(
                    message: "Проекта не существует",
                    entityName: $"{nameof(Order)}"
                );

            return result;
        }

        private TComponent GetComponentFrom<TComponent>(Guid componentID)
            where TComponent : Component
        {
            var result = _repository.GetByPrimaryKey<TComponent>(componentID);

            if (result == null)
                throw new EntityDoesNotExistException(
                    message: "Компонента не существует",
                    entityName: $"{nameof(Component)}"
                );

            return result;
        }

        private Work GetWorkFrom(Guid workID)
        {
            var result = _repository.GetByPrimaryKey<Work>(workID);

            if (result == null)
                throw new EntityDoesNotExistException(
                    message: "Попытка добавить работу, которой не сущесвтует",
                    entityName: $"{nameof(Work)}"
                );

            return result;
        }

        private void ValidateWorkPerimeter(Guid orderID, double perimeter)
        {
            var orderPerimeter = _repository.
                Get<OrderPerimeter>(op => op.OrderID == orderID).
                    Sum(op => op.Perimeter);
            if (orderPerimeter < perimeter)
                throw new ArgumentException(
                    "Нельзя добавить работу с периметром, большим чем периметр всего проекта " +
                    $"({perimeter} > {orderPerimeter})"
                );
        }

        private void ValidateOrder(Order order)
        {
            var maxDescriptionSymbols = 255;
            var minOrderNameLength = 3;
            var maxOrderNameLength = 30;

            if (order == null)
                throw new NullReferenceException(
                    "Проект не может быть быть null"
                );

            var badNameException = new BadEntityPropertyException(
                    message: $"Длина названия проекта должна составлять от {minOrderNameLength} до {maxOrderNameLength} символов",
                    entityName: $"{nameof(Order)}",
                    propertyName: $"{nameof(order.Name)}"
                );

            if (order.Name == null) throw badNameException;

            if (order.Name.Length < minOrderNameLength || order.Name.Length > maxOrderNameLength)
                throw badNameException;

            if (_repository.GetFirst<Order>(o => o.Name == order.Name) != null)
                throw new EntityAlreadyExistsException(
                    message: $"Проект с названием \"{order.Name}\" уже существует",
                    propertyName: $"{nameof(order.Name)}"
                );

            if (order.Desription?.Length > maxDescriptionSymbols)
                throw new BadEntityPropertyException(
                    message: $"Описание проекта не должно превышать {maxDescriptionSymbols} символов",
                    entityName: $"{nameof(Order)}",
                    propertyName: $"{nameof(order.Desription)}"
                );
        }

        private void ValidatePerimeterType(Guid perimeterType)
        {
            var result = _repository.GetByPrimaryKey<PerimeterType>(perimeterType);

            if (result == null)
                throw new EntityDoesNotExistException(
                    message: "Тип периметра с указанным ID не существует",
                    entityName: $"{nameof(PerimeterType)}"
                );
        }
    }
}
