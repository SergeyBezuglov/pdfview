using Mapster;
using MapsterMapper;
using System.Reflection;

namespace PIMS.Web.Common.Mapping
{
    /// <summary>
    /// Внедрение зависимостей.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Добавьте сопоставления.
        /// </summary>
        /// <param name="services">Услуги.</param>
        /// <returns>Возвращение значения сбора услуг (IServiceCollection).</returns>
        public static IServiceCollection  AddMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddControllers();
            services.AddSingleton(config);
            services.AddScoped<IMapper,ServiceMapper>();
            return services;
        }
    }
}
