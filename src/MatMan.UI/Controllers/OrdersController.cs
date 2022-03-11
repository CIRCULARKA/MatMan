using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using MatMan.UI.ViewModels;
using MatMan.Application.Editors;
using MatMan.Application.Reports;
using MatMan.Application.Providers;
using MatMan.Domain.Models;

namespace MatMan.UI.Controllers
{
    public class OrdersController : ApplicationController
    {
        private readonly IOrdersProvider _ordersProvider;

        private readonly IWorksProvider _worksProvider;

        private readonly IComponentsProvider<Ware, WareConfiguration> _waresProvider;

        private readonly IComponentsProvider<Material, MaterialConfiguration> _materialsProvider;

        private readonly IOrdersEditor _ordersEditor;

        private readonly ILogger<OrdersController> _logger;

        private readonly IFileProvider _fileProvider;

        private readonly OrderSummaryPdfGenerator _pdfGenerator;

        public OrdersController(
            IOrdersProvider ordersProvider,
            IOrdersEditor ordersEditor,
            IWorksProvider worksProvider,
            IComponentsProvider<Ware, WareConfiguration> waresProvider,
            IComponentsProvider<Material, MaterialConfiguration> matsProvider,
            ILogger<OrdersController> logger,
            IFileProvider fileProvider,
            OrderSummaryPdfGenerator generator)
        {
            _ordersProvider = ordersProvider;
            _waresProvider = waresProvider;
            _worksProvider = worksProvider;
            _ordersEditor = ordersEditor;
            _materialsProvider = matsProvider;

            _logger = logger;

            _fileProvider = fileProvider;

            _pdfGenerator = generator;
        }

        public IActionResult CreateOrder(CreateOrderViewModel vm)
        {
            var orderCopy = new Order {
                Name = vm.NewOrder.Name,
                Desription = vm.NewOrder.Desription
            };

            try
            {
                _ordersEditor.CreateOrder(orderCopy, vm.NewOrderPerimeters);

                foreach (var selectedWare in vm.SelectableWares)
                    if (selectedWare.IsSelected)
                        _ordersEditor.AddWare(
                            orderCopy.ID,
                            selectedWare.EntityID,
                            (int)selectedWare.Amount
                        );

                foreach (var selectedWork in vm.SelectableWorks)
                {
                    if (!selectedWork.IsSelected) continue;
                    if (_worksProvider.GetWorkByID(selectedWork.EntityID).IsApplicableToWholePerimeter)
                        foreach (var perimeterType in vm.NewOrderPerimeters)
                            _ordersEditor.AddWork(
                                orderCopy.ID,
                                selectedWork.EntityID,
                                perimeterType.PerimeterTypeID,
                                perimeterType.Perimeter ?? 0
                            );
                    else
                        _ordersEditor.AddWork(
                            orderCopy.ID,
                            selectedWork.EntityID,
                            vm.NewOrderPerimeters[0].PerimeterTypeID,
                            selectedWork.Amount == 0 ? 1 : selectedWork.Amount
                        );
                }

                SuccessMessage = $"Проект \"{vm.NewOrder.Name}\" успешно создан";
            }
            catch (Exception e)
            {
                _ordersEditor.DeleteOrder(orderCopy.ID);

                ErrorMessage = e.Message;
            }

            return View(
                "CreateOrder",
                GetUpdatedCreateOrderViewModel()
            );
        }

        public IActionResult DeleteOrder(Guid orderID)
        {
            var targetOrder = _ordersProvider.GetOrderBy(orderID);
            if (targetOrder != null)
            {
                _ordersEditor.DeleteOrder(targetOrder.ID);
                SuccessMessage = $"Проект \"{targetOrder.Name}\" успешно удалён";
            }

            return View(
                "OrdersList",
                GetUpdatedOrdersListViewModel()
            );
        }

        public IActionResult GetOrderSummaryView(Guid orderID) =>
            View(
                "OrderSummary",
                GetUpdatedOrderSummaryViewModel(orderID)
            );

        public IActionResult GetCreateOrderView() =>
            View(
                "CreateOrder",
                GetUpdatedCreateOrderViewModel()
            );

        public IActionResult GetOrdersListView() =>
            View(
                "OrdersList",
                GetUpdatedOrdersListViewModel()
            );

        public IActionResult DownloadAsPDF(Guid orderID)
        {
            var targetOrder = _ordersProvider.GetOrderBy(orderID);
            var materials = _ordersProvider.GetOrderComponents<Material>(orderID);

            return File(
                fileContents: _pdfGenerator.GenerateReport(targetOrder.Name, materials),
                contentType: "application/octet-stream",
                fileDownloadName: $"{targetOrder.Name}.pdf"
            );
        }

        private OrderSummaryViewModel GetUpdatedOrderSummaryViewModel(Guid orderID) =>
            new OrderSummaryViewModel {
                Order = _ordersProvider.GetOrderBy(orderID),
                OrderPerimeters = _ordersProvider.GetOrderPerimeters(orderID).
                    ToList(),
                AllUsedWares = _ordersProvider.GetOrderComponents<Ware>(orderID).
                    ToList(),
                AllUsedMaterials = _ordersProvider.GetOrderComponents<Material>(orderID).
                    ToList(),
                UsedWorks = _ordersProvider.GetOrderWorks(orderID).
                    ToList(),
                MaterialConfigurations = _materialsProvider.AllComponentsConfigurations.
                    ToList(),
                WaresConfigurations = _waresProvider.AllComponentsConfigurations.
                    ToList()
            };

        private CreateOrderViewModel GetUpdatedCreateOrderViewModel()
        {
            List<Work> allWorks = _worksProvider.AllWorks.ToList();
            List<Ware> allWares = _waresProvider.AllComponents.ToList();
            List<PerimeterType> allPerimeterTypes = _ordersProvider.AllPerimeterTypes.ToList();
            List<WareConfiguration> waresConfigurations = _waresProvider.AllComponentsConfigurations.ToList();

            return new CreateOrderViewModel {
                SelectableWares = new SelectableEntity<Ware>[allWares.Count],
                SelectableWorks = new SelectableEntity<Work>[allWorks.Count],
                NewOrderPerimeters = new OrderPerimeter[allPerimeterTypes.Count],
                Wares = allWares,
                Works = allWorks,
                PerimeterTypes = allPerimeterTypes,
                WaresConfigurations = waresConfigurations
            };
        }

        private OrdersListViewModel GetUpdatedOrdersListViewModel() =>
            new OrdersListViewModel {
                Entities = _ordersProvider.AllOrders.ToList()
            };
    }
}
