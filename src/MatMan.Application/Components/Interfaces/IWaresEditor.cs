using System;
using MatMan.Domain.Models;

namespace MatMan.Application.Editors
{
    public interface IWaresEditor :
        IComponentsEditor<Ware, WareConfiguration>
    {
        void AddMaterial(Guid wareID, Guid materialID, int materialAmount);
    }
}
