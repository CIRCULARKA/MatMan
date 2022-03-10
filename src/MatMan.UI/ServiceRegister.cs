using MatMan.Application.Providers;
using MatMan.Application.Editors;
using MatMan.Application.Reports;
using MatMan.Data;
using MatMan.Domain.Models;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            RegisterRepositories(services);
            RegisterUnitsOfWork(services);

            return services;
        }

        private static void RegisterRepositories(IServiceCollection services) =>
            services.AddScoped<IRepository, Repository>();

        private static void RegisterUnitsOfWork(IServiceCollection services)
        {
            services.AddScoped<
                IComponentsProvider<Material, MaterialConfiguration>,
                ComponentsProvider<Material, MaterialConfiguration>
            >();
            services.AddScoped<
                IComponentsEditor<Material, MaterialConfiguration>,
                ComponentsEditor<Material, MaterialConfiguration>
            >();

            services.AddScoped<
                IComponentsProvider<Ware, WareConfiguration>,
                ComponentsProvider<Ware, WareConfiguration>
            >();
            services.AddScoped<
                IComponentsEditor<Ware, WareConfiguration>,
                ComponentsEditor<Ware, WareConfiguration>
            >();

            services.AddScoped<MaterialsProvider>();

            services.AddScoped<IWaresEditor, WaresEditor>();
            services.AddScoped<WaresProvider>();

            services.AddScoped<IOrdersProvider, OrdersProvider>();
            services.AddScoped<IOrdersEditor, OrdersEditor>();

            services.AddScoped<IWorksProvider, WorksProvider>();

            services.AddScoped<PdfGenerator>();
        }
    }
}
