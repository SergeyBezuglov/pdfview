using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Nest;
using PIMS.Application;
using PIMS.Infrastructure;
using PIMS.Infrastructure.Persistence.DbContexts;
using PIMS.Web;
using PIMS.Web.Common.Errors;
using PIMS.Web.Extensions;
using PIMS.Web.Filters;
using PIMS.Web.Helpers;
using PIMS.Web.Middleware;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Net;
using Vite.AspNetCore.Extensions;
using DependencyInjection = PIMS.Web.DependencyInjection;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
	Args = args


});
var settings = new ConnectionSettings(new Uri("http://localhost:9200")) // Укажите правильный URI к вашему Elasticsearch
    .DefaultIndex("your_default_index"); // Укажите индекс по умолчанию

var client = new ElasticClient(settings);
builder.Services.AddSingleton<IElasticClient>(client);
{   
    builder.Services.
    AddPresentation(builder).
    AddApplication().
    AddInfrastracture(builder); 
}

var app = builder.Build();
{
     
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    
    app.MigrateDatabase();

    app.UseExceptionHandler("/error");

    app.UseSwaggerExtension(provider);

    app.UseSwaggerUI();


    app.UseStaticFiles(new StaticFileOptions()
	{
		FileProvider = new PhysicalFileProvider(
			Path.Combine(builder.Environment.ContentRootPath, @"Client")),
		RequestPath = new PathString("/StaticFiles")
	});
	try
	{
		app.UseSpaStaticFiles();
	}
	catch (Exception exp)
	{
		Log.Fatal(exp, "While loading spa static files module");
	}
	app.UseHttpsRedirection();

    app.UseRouting();
    app.UseCors(b =>
    {
        b.AllowAnyOrigin();
        b.AllowAnyHeader();
        b.AllowAnyMethod();
    });
    app.UseAuthentication();

    app.UseAuthorization();
    app.UseMiddleware<ValidateAuthentication>();

    //app.UseStatusCodePages(async context => {
    //    var request = context.HttpContext.Request;
    //    var response = context.HttpContext.Response;

    //    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)      
    //    {
    //        string str = "";
    //       }
    //});
  
    
   

    app.MapControllers();
    app.ConfigureSPA();
    app.UseStaticFiles();
    app.UseCors("MyPolicy");
    app.Run();


}
public partial class Program { }