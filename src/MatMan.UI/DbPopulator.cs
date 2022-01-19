using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using MatMan.Data;
using MatMan.Domain.Models;

namespace MatMan.UI
{
    public static class DbPopulator
    {
        public static IApplicationBuilder PopulateDatabase(
            this IApplicationBuilder builder)
        {
            var context = builder.ApplicationServices.
                CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            if (context == null) return builder;
            if (context.Works.Any()) return builder;

            var perimeterTypes = new PerimeterType[] {
                CreatePerimeterType("Периметр бетонных стен"),
                CreatePerimeterType("Периметр небетонных стен")
            };

            context.PerimeterTypes.AddRange(perimeterTypes);

            var units = new Unit[] {
                CreateUnit("м", "метр", 1),
                CreateUnit("см", "сантиметр", 0.01),
                CreateUnit("мм", "миллиметр", 0.001)
            };

            context.Units.AddRange(units);
            context.SaveChanges();

            var materials = new Material[] {
                CreateMaterial("Дюбель"),
                CreateMaterial("Саморез"),
                CreateMaterial("Уголок"),
                CreateMaterial("Платформа"),
                CreateMaterial("Кольцо"),
                CreateMaterial("Подвес"),
                CreateMaterial("Провод"),
                CreateMaterial("Саморез \"Семечка\""),
                CreateMaterial("Изоляция \"Сиза\""),
                CreateMaterial("Хомут"),
                CreateMaterial("Перо"),
                CreateMaterial("Кантик"),
                CreateMaterial("Трубоотвод"),
                CreateMaterial("Рассеиватель"),
                CreateMaterial("Гарпун"),
                CreateMaterial("Консоль"),
                CreateMaterial("Бондажная лента"),
                CreateMaterial("Светодиоидная лента"),
                CreateMaterial("Трансформатор")
            };

            context.Materials.AddRange(materials);

            var materialsConfigurations = new MaterialConfiguration[] {
                CreateMaterialConfiguration(materials[6].ID, units[0].ID),
                CreateMaterialConfiguration(materials[11].ID, units[0].ID),
            };

            context.MaterialsConfigurations.AddRange(materialsConfigurations);
            context.SaveChanges();

            var wares = new Ware[] {
                CreateWare("Багет"),
                CreateWare("Брус"),
                CreateWare("Светильник"),
                CreateWare("Труба"),
                CreateWare("Световая линия")
            };

            context.Wares.AddRange(wares);

            context.SaveChanges();

            var waresMaterials = new WareMaterial[] {
                CreateWareMaterial(wares[0].ID, materials[0].ID, 20),
                CreateWareMaterial(wares[0].ID, materials[1].ID, 5),

                CreateWareMaterial(wares[1].ID, materials[1].ID, 6),
                CreateWareMaterial(wares[1].ID, materials[2].ID, 2),

                CreateWareMaterial(wares[2].ID, materials[3].ID, 1),
                CreateWareMaterial(wares[2].ID, materials[4].ID, 1),
                CreateWareMaterial(wares[2].ID, materials[5].ID, 1),
                CreateWareMaterial(wares[2].ID, materials[6].ID, 1.5f),
                CreateWareMaterial(wares[2].ID, materials[7].ID, 4),
                CreateWareMaterial(wares[2].ID, materials[8].ID, 2),
                CreateWareMaterial(wares[2].ID, materials[9].ID, 1),

                CreateWareMaterial(wares[3].ID, materials[12].ID, 1),

                CreateWareMaterial(wares[4].ID, materials[13].ID, 3),
                CreateWareMaterial(wares[4].ID, materials[14].ID, 5),
                CreateWareMaterial(wares[4].ID, materials[15].ID, 6),
                CreateWareMaterial(wares[4].ID, materials[1].ID, 20),
                CreateWareMaterial(wares[4].ID, materials[16].ID, 1),
                CreateWareMaterial(wares[4].ID, materials[17].ID, 2),
                CreateWareMaterial(wares[4].ID, materials[18].ID, 1)
            };

            context.WaresMaterials.AddRange(waresMaterials);

			var works = new Work[] {
                CreateWork("Использование кантика", true, 1),
                CreateWork("Работа по плитке", true, 5),
                CreateWork("Отступы у потолка", false, 1),
                CreateWork("Закладная", true, 1),
                CreateWork("Использование багета", true, 2.5)
            };

            context.Works.AddRange(works);
            context.SaveChanges();

            var worksMaterials = new WorkMaterial[] {
                CreateWorkMaterial(works[0].ID, materials[11].ID, 1, perimeterTypes[0].ID),
                CreateWorkMaterial(works[0].ID, materials[11].ID, 1, perimeterTypes[1].ID),

                CreateWorkMaterial(works[1].ID, materials[10].ID, 1, perimeterTypes[0].ID),
                CreateWorkMaterial(works[1].ID, materials[10].ID, 1, perimeterTypes[1].ID),

                CreateWorkMaterial(works[2].ID, materials[1].ID, 6, perimeterTypes[0].ID),
                CreateWorkMaterial(works[2].ID, materials[2].ID, 2, perimeterTypes[0].ID),
                CreateWorkMaterial(works[2].ID, materials[1].ID, 6, perimeterTypes[1].ID),
                CreateWorkMaterial(works[2].ID, materials[2].ID, 2, perimeterTypes[1].ID),

                CreateWorkMaterial(works[3].ID, materials[1].ID, 10, perimeterTypes[0].ID),
                CreateWorkMaterial(works[3].ID, materials[5].ID, 2, perimeterTypes[0].ID),
                CreateWorkMaterial(works[3].ID, materials[1].ID, 10, perimeterTypes[1].ID),
                CreateWorkMaterial(works[3].ID, materials[5].ID, 2, perimeterTypes[1].ID),

                CreateWorkMaterial(works[4].ID, materials[0].ID, 20, perimeterTypes[0].ID),
                CreateWorkMaterial(works[4].ID, materials[1].ID, 5, perimeterTypes[0].ID),
                CreateWorkMaterial(works[4].ID, materials[1].ID, 25, perimeterTypes[1].ID),
           };

           context.WorksMaterials.AddRange(worksMaterials);

            var worksWares = new WorkWare[] {
                CreateWorkWare(works[2].ID, wares[1].ID, 1, perimeterTypes[0].ID),

                CreateWorkWare(works[4].ID, wares[0].ID, 1, perimeterTypes[0].ID),
                CreateWorkWare(works[4].ID, wares[0].ID, 1, perimeterTypes[1].ID)
            };

            context.WorksWares.AddRange(worksWares);

            context.SaveChanges();

            context.Dispose();

            return builder;
        }

        private static PerimeterType CreatePerimeterType(string name) =>
            new PerimeterType { ID = Guid.NewGuid(), Name = name };

        private static Unit CreateUnit(string shortName, string fullName, double attitudeToMeter) =>
            new Unit { ID = Guid.NewGuid(),
                ShortName = shortName,
                FullName = fullName,
                AttitudeToMeter = attitudeToMeter
            };

        private static Material CreateMaterial(string name) =>
            new Material { ID = Guid.NewGuid(), Name = name };

        private static MaterialConfiguration CreateMaterialConfiguration(Guid materialID, Guid unitID) =>
            new MaterialConfiguration { ID = Guid.NewGuid(),
                ComponentID = materialID,
                UnitID = unitID,
            };

        private static Ware CreateWare(string name) =>
            new Ware { ID = Guid.NewGuid(), Name = name };

        private static WareMaterial CreateWareMaterial(Guid wareID, Guid materialID, double materialAmount) =>
            new WareMaterial { ID = Guid.NewGuid(),
                WareID = wareID,
                MaterialID = materialID,
                MaterialsAmount = materialAmount
            };

        private static Work CreateWork(string name, bool isApplicableToWholePerimeter, double applicableLength) =>
            new Work {
                ID = Guid.NewGuid(),
                Name = name,
                IsApplicableToWholePerimeter = isApplicableToWholePerimeter,
                ApplicableLength = applicableLength
            };

        private static WorkMaterial CreateWorkMaterial(Guid workID, Guid materialID, int materialAmount, Guid perimeterTypeID) =>
            new WorkMaterial {
                ID = Guid.NewGuid(),
                WorkID = workID,
                MaterialID = materialID,
                MaterialsAmount = materialAmount,
                PerimeterTypeID = perimeterTypeID
            };

        private static WorkWare CreateWorkWare(Guid workID, Guid wareID, int wareAmount, Guid perimeterTypeID) =>
            new WorkWare {
                ID = Guid.NewGuid(),
                WorkID = workID,
                WareID = wareID,
                WaresAmount = wareAmount,
                PerimeterTypeID = perimeterTypeID
            };

        private static WorkRule CreateWorkRule(Guid workID, double additionalPerimeter) =>
            new WorkRule {
                ID = Guid.NewGuid(),
                WorkID = workID,
                AdditionalPerimeter = additionalPerimeter
            };
    }
}
