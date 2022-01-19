using System;
using MatMan.Data;
using MatMan.Domain.Models;

namespace MatMan.Application.Editors
{
    public class WaresEditor : ComponentsEditor<Ware, WareConfiguration>, IWaresEditor
    {
        public WaresEditor(
            IRepository repo
        ) : base(repo) { }

        public void AddMaterial(Guid wareID, Guid materialID, int materialAmount)
        {
            ValidateMaterial(materialID);

            _repository.Add(new WareMaterial {
                MaterialID = materialID,
                WareID = wareID,
                MaterialsAmount = materialAmount
            });
            _repository.Save();
        }

        private void ValidateMaterial(Guid materialID)
        {
            if (_repository.GetByPrimaryKey<Material>(materialID) == null)
                throw new EntityDoesNotExistException(
                    message: $"Не удалсь добавить материал к изделию - в базе данных нет материала с указанным ID",
                    entityName: $"{nameof(Material)}"
                );
        }
    }
}
