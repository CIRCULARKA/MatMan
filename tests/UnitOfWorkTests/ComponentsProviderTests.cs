using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Moq;
using MatMan.Data;
using MatMan.Application.Providers;
using MatMan.Domain.Models;

namespace MatMan.Tests
{
    public class ComponentsProviderTests
    {
        private IRepository _repoMock;

        private List<Material> _expectedMaterials;

        private List<Ware> _expectedWares;

        public ComponentsProviderTests()
        {
            _expectedMaterials = new List<Material> {
                new Material { ID = Guid.NewGuid(), Name = "mat1" },
                new Material { ID = Guid.NewGuid(), Name = "mat2" }
            };

            _expectedWares = new List<Ware> {
                new Ware { ID = Guid.NewGuid(), Name = "ware1" },
                new Ware { ID = Guid.NewGuid(), Name = "ware2" }
            };

            var repoMock = new Mock<IRepository>();
            repoMock.
                Setup(r => r.Get<Material>(null, null, "")).
                    Returns(_expectedMaterials);

            repoMock.
                Setup(r => r.Get<Ware>(null, null, "")).
                    Returns(_expectedWares);

            _repoMock = repoMock.Object;
        }

        [Fact]
        public void IsMaterialsPullsRight()
        {
            // Arrange
            var materialsProvider = new ComponentsProvider<Material, MaterialConfiguration>(
                repo: _repoMock
            );

            // Act
            var result = materialsProvider.AllComponents.ToList();

            // Assert
            Assert.Equal(_expectedMaterials.Count, result.Count);

            for (int i = 0; i < result.Count; i++)
            {
                Assert.Equal(_expectedMaterials[i].ID, result[i].ID);
                Assert.Equal(_expectedMaterials[i].Name, result[i].Name);
            }
        }

        [Fact]
        public void IsWaresPullsRight()
        {
            // Arrange
            var materialsProvider = new ComponentsProvider<Ware, WareConfiguration>(
                _repoMock
            );

            // Act
            var result = materialsProvider.AllComponents.ToList();

            // Assert
            Assert.Equal(_expectedMaterials.Count, result.Count);

            for (int i = 0; i < result.Count; i++)
            {
                Assert.Equal(_expectedWares[i].ID, result[i].ID);
                Assert.Equal(_expectedWares[i].Name, result[i].Name);
            }
        }
    }
}
