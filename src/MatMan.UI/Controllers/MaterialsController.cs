using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MatMan.UI.ViewModels;
using MatMan.Application.Editors;
using MatMan.Application.Providers;
using MatMan.Domain.Models;

namespace MatMan.UI.Controllers
{
    public class MaterialsController : ApplicationController
    {
        private MaterialsProvider _materialsProvider;

        private IComponentsEditor<Material, MaterialConfiguration> _materialsEditor;

        public MaterialsController(
            MaterialsProvider materialsProvider,
            IComponentsEditor<Material, MaterialConfiguration> materialsEditor)
        {
            _materialsProvider = materialsProvider;
            _materialsEditor = materialsEditor;
        }

        [HttpGet]
        public IActionResult GetMaterialsListView() =>
            View(
                "MaterialsList",
                GetUpdatedViewModel()
            );

        [HttpPost]
        public IActionResult CreateMaterial(MaterialsViewModel vm)
        {
            var newMaterialID = Guid.Empty;

            try
            {
                if (vm.NewEntityConfiguration.UnitID == Guid.Empty)
                    newMaterialID = _materialsEditor.CreateComponent(vm.NewEntity);
                else
                    newMaterialID = _materialsEditor.CreateComponent(vm.NewEntity, vm.NewEntityConfiguration);
            }
            catch (Exception e)
            {
                _materialsEditor.DeleteComponent(newMaterialID);

                ErrorMessage = e.Message;
            }

            return View(
                "MaterialsList",
                GetUpdatedViewModel()
            );
        }

        [HttpGet]
        public IActionResult DeleteMaterial(Guid materialID)
        {
            var materialToDelete = _materialsProvider.GetComponentByID(materialID);

            if (materialToDelete == null) View("MaterialsList", GetUpdatedViewModel());

            try
            {
                var waresMaterialUsedIn = _materialsProvider.GetWaresMaterialUsedIn(materialID).ToList();
                var worksMaterialUsedIn = _materialsProvider.GetWorksMaterialUsedIn(materialID).ToList();

                if (waresMaterialUsedIn.Count > 0)
                    throw new Exception(
                        BuildErrorMessage(
                            "Невозможно удалить материал, так как он используется в других изделиях",
                            waresMaterialUsedIn.Select(w => w.Name)
                        )
                    );

                if (worksMaterialUsedIn.Count > 0)
                    throw new Exception(
                        BuildErrorMessage(
                            "Невозможно удалить материал, так как он используется в других работах",
                            worksMaterialUsedIn.Select(w => w.Name)
                        )
                    );

                _materialsEditor.DeleteComponent(materialID);
                SuccessMessage = $"Материал \"{materialToDelete.Name}\" удалён успешно";
            }
            catch (Exception e) { ErrorMessage = e.Message; }

            return View(
                "MaterialsList",
                GetUpdatedViewModel()
            );
        }

        private MaterialsViewModel GetUpdatedViewModel() =>
            new MaterialsViewModel {
                Entities = _materialsProvider.AllComponents.ToList(),
                Units = _materialsProvider.AllUnits.ToList(),
                EntitiesConfigurations = _materialsProvider.AllComponentsConfigurations.ToList()
            };
    }
}
