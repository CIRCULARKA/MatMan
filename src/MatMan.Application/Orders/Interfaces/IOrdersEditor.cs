using System;
using System.Collections.Generic;
using MatMan.Domain.Models;

namespace MatMan.Application.Editors
{
    public interface IOrdersEditor
    {
        void CreateOrder(Order newOrder, IEnumerable<OrderPerimeter> orderPerimeters);

        void DeleteOrder(Guid orderID);

        void AddMaterial(Guid orderID, Guid materialID, double materialsAmount);

        void AddWare(Guid orderID, Guid wareID, int waresAmount);

        void AddWork(Guid orderID, Guid workID, Guid perimeterTypeID, double workPerimeter);
    }
}
