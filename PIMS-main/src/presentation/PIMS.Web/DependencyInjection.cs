using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PIMS.Web.Common.Errors;
using PIMS.Web.Common.Mapping;

using PIMS.Web.Extensions;
using PIMS.Web.Helpers;
using Swashbuckle.AspNetCore.SwaggerGen;
using PIMS.Web.Middleware;
using Vite.AspNetCore.Extensions;
using System.Text.Json.Serialization;
using PIMS.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace PIMS.Web
{
    /// <summary>
    /// Внедрение зависимостей.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Путь для создания сайта документа.
        /// </summary>
        public static readonly string PathToGenerateDocSite = "..\\..\\..\\documentation\\docfx_project\\docfx.json";
        /// <summary>
        /// Добавить презентацию.
        /// </summary>
        /// <param name="services">Услуги.</param>
        /// <param name="builder">Строитель.</param>
        /// <returns>Возвращение значения сбора услуг (IServiceCollection).</returns>
        public static IServiceCollection AddPresentation(this IServiceCollection services,WebApplicationBuilder builder) {

            services.AddMappings(). 
        AddEndpointsApiExplorer().
        AddApiVersioningExtension().
        AddVersionedApiExplorerExtension().
        AddSwaggerGenExtension();

            
            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.WriteIndented = true;

                // serialize enums as strings in api responses (e.g. Role)
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                // ignore omitted parameters on models to enable optional params (e.g. User update)
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            }); 

            services.AddSpaStaticFiles(options =>
            {
                options.RootPath = "client/dist";
            });
            services.AddViteServices();
      



            services.AddCors();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSingleton<ProblemDetailsFactory, PIMSProblemDetailsFactory>();
            services.AddSingleton<ValidateAuthentication>();

            services.Configure<ApiBehaviorOptions>(options =>
               options.SuppressModelStateInvalidFilter = true
           );

           


            return services;
        }

        /// <summary>
        /// Мигрирует базу данных.
        /// </summary>
        /// <param name="app">Приложение.</param>
        public static void MigrateDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider
                    .GetRequiredService<PIMSDbContext>();
                dbContext.Database.Migrate();
            }        
        }
        /// <summary>
        /// Настраивает SPA.
        /// </summary>
        /// <param name="app">Приложение.</param>
        public static void ConfigureSPA(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseViteDevMiddleware();
            }
            else
            {
                app.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "client";
                });
            }
        }
    }
}
