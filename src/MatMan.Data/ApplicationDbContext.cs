using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MatMan.Domain.Models;
using MatMan.Data.Attributes;

namespace MatMan.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options) { }

        public DbSet<PerimeterType> PerimeterTypes { get; set; }

        public DbSet<Unit> Units { get; set; }

        public DbSet<Material> Materials { get; set; }

        public DbSet<Ware> Wares { get; set; }

        public DbSet<Work> Works { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<MatMan.Domain.Models.MaterialConfiguration> MaterialsConfigurations { get; set; }

        public DbSet<MatMan.Domain.Models.WareConfiguration> WaresConfigurations { get; set; }

        public DbSet<WareMaterial> WaresMaterials { get; set; }

        public DbSet<WorkMaterial> WorksMaterials { get; set; }

        public DbSet<OrderComponent<Material>> OrdersMaterials { get; set; }

        public DbSet<OrderComponent<Ware>> OrdersWares { get; set; }

        public DbSet<WorkWare> WorksWares { get; set; }

        public DbSet<WorkRule> WorkRules { get; set; }

        public DbSet<OrderWork> OrdersWorks { get; set; }

        public DbSet<OrderPerimeter> OrdersPerimeters { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(
                typeof(ModelConfigurationAttribute).Assembly
            );
        }
    }
}
