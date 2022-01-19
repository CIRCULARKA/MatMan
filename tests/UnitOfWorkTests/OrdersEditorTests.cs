using System;
using Moq;
using Xunit;
using MatMan.Data;
using MatMan.Domain.Models;
using MatMan.Application.Editors;
using MatMan.Application.Providers;

namespace MatMan.Tests
{
    public class OrdersEditorTests
    {
        private readonly IRepository _repoMock;

        private readonly Material[] _testMaterials;

        private readonly Ware[] _testWares;

        private readonly OrderComponent<Material>[] _testOrderMaterials;

        private readonly OrderComponent<Ware>[] _testOrderWares;

        private readonly Order _targetOrder;

        public OrdersEditorTests()
        {
            _targetOrder = new Order {
                ID = Guid.NewGuid(),
                Name = "order",
                Desription = "test order"
            };

            _testMaterials = new Material[] {
                new Material { ID = Guid.NewGuid(), Name = "mat1" },
                new Material { ID = Guid.NewGuid(), Name = "mat2" },
                new Material { ID = Guid.NewGuid(), Name = "mat3" },
            };

            _testWares = new Ware[] {
                new Ware { ID = Guid.NewGuid(), Name = "ware1" },
                new Ware { ID = Guid.NewGuid(), Name = "ware2" },
                new Ware { ID = Guid.NewGuid(), Name = "ware3" }
            };

            _testOrderMaterials = new OrderComponent<Material>[] {
                new OrderComponent<Material> {
                    ID = Guid.NewGuid(),
                    OrderID = _targetOrder.ID,
                    ComponentID = _testMaterials[0].ID,
                    ComponentAmount = 3
                }
            };

            _testOrderWares = new OrderComponent<Ware>[] {
                new OrderComponent<Ware> {
                    ID = Guid.NewGuid(),
                    OrderID = _targetOrder.ID,
                    ComponentID = _testWares[0].ID,
                    ComponentAmount = 2
                }
            };

            var repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.Get<Material>(null, null, null)).
                Returns(_testMaterials);
            repoMock.Setup(r => r.Get<Ware>(null, null, null)).
                Returns(_testWares);
            repoMock.Setup(r => r.Get<OrderComponent<Material>>(null, null, null)).
                Returns(_testOrderMaterials);

            _repoMock = repoMock.Object;
        }

        [Fact]
        public void IsComponentsAddedProperly()
        {
            // Arrange
            var editor = new OrdersEditor(_repoMock);

            // Act
            editor.AddMaterial(_targetOrder.ID, _testMaterials[0].ID, 2);
            editor.AddMaterial(_targetOrder.ID, _testMaterials[1].ID, 3);

            // Assert
            // Assert.Equal();
        }
    }
}
