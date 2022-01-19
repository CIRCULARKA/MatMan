using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MatMan.Application.Editors;
using MatMan.Application.Providers;
using MatMan.Domain.Models;
using MatMan.UI.ViewModels;

namespace MatMan.UI.Controllers
{
    public class WaresController : ApplicationController
    {
        private readonly IComponentsProvider<Material, MaterialConfiguration> _materialsProvider;

        private readonly WaresProvider _waresProvider;

        private readonly IWaresEditor _waresEditor;

        private readonly ILogger<WaresController> _logger;

        public WaresController(
            IComponentsProvider<Material, MaterialConfiguration> matsProvider,
            WaresProvider waresProvider,
            IWaresEditor waresEditor,
            ILogger<WaresController> logger
        )
        {
            _materialsProvider = matsProvider;
            _waresProvider = waresProvider;
            _waresEditor = waresEditor;

            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetWaresListView()
        {
            try
            {
                return View(
                    "WaresList",
                    GetUpdatedViewModel()
                );
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return View(
                    "WaresList",
                    GetUpdatedViewModel()
                );
            }
        }

        [HttpPost]
        public IActionResult CreateWare(WaresViewModel vm)
        {
            var newWareID = Guid.Empty;

            try
            {
                if (vm.NewEntityConfiguration.UnitID == Guid.Empty)
                    newWareID = _waresEditor.CreateComponent(vm.NewEntity);
                else
                    newWareID = _waresEditor.CreateComponent(vm.NewEntity, vm.NewEntityConfiguration);

                if (vm.SelectableMaterials.Where(sm => sm.IsSelected == true).Count() == 0)
                    throw new Exception("В состав изделия должен входить хотя-бы один материал");

                if (vm.SelectableMaterials != null)
                {
                    var newWare = _waresProvider.GetComponentByName(vm.NewEntity.Name);

                    foreach (var selectableMaterial in vm.SelectableMaterials)
                    {
                        if (selectableMaterial.IsSelected)
                            _waresEditor.AddMaterial(
                                newWare.ID,
                                selectableMaterial.EntityID,
                                (int)selectableMaterial.Amount
                            );
                    }
                }

                SuccessMessage = $"Изделие успешно создано";
            }
            catch (Exception e)
            {
                _waresEditor.DeleteComponent(newWareID);

                ErrorMessage = e.Message;
            }

            return View(
                "WaresList",
                GetUpdatedViewModel()
            );
        }

        public IActionResult DeleteWare(Guid wareID)
        {
            var wareToDelete = _waresProvider.GetComponentByID(wareID);

            var result = View("WaresList", GetUpdatedViewModel());

            if (wareToDelete == null) return View("WaresList", GetUpdatedViewModel());

            var ordersWareUsedIn = _waresProvider.GetOrdersWareUsedIn(wareID).ToList();
            var worksWareUsedIn = _waresProvider.GetWorksWareUsedIn(wareID).ToList();

            try
            {
                if (ordersWareUsedIn.Count != 0)
                    throw new Exception(
                        BuildErrorMessage(
                            "Невозможно удалить изделие, так как оно используется в проектах",
                            ordersWareUsedIn.Select(o => o.Name)
                        )
                    );

                if (worksWareUsedIn.Count != 0)
                    throw new Exception(
                        BuildErrorMessage(
                            "Невозможно удалить изделие, так как оно используется в работах",
                            worksWareUsedIn.Select(o => o.Name)
                        )
                    );

                _waresEditor.DeleteComponent(wareToDelete.ID);
                SuccessMessage = $"Изделие \"{wareToDelete.Name}\" удалено";
            }
            catch (Exception e) { ErrorMessage = e.Message; }

            return View(
                "WaresList",
                GetUpdatedViewModel()
            );
        }

        private WaresViewModel GetUpdatedViewModel() =>
            new WaresViewModel {
                Entities = _waresProvider.AllComponents.OrderBy(m => m.ID).ToList(),
                EntitiesConfigurations = _waresProvider.AllComponentsConfigurations.ToList(),
                Units = _waresProvider.AllUnits.ToList(),
                Materials = _materialsProvider.AllComponents.ToList(),
                MaterialsConfigurations = _materialsProvider.AllComponentsConfigurations.ToList(),
                WaresMaterials = _waresProvider.GetMaterials().ToList(),
                SelectableMaterials = new SelectableEntity<Material>[
                    _materialsProvider.AllComponents.Count()
                ]
            };
    }
}
