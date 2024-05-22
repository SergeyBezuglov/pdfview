using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PIMS.Domain;
using PIMS.Infrastructure.Persistence.DbContexts;

namespace PIMS.Application.IntegrationTests.PdfSearch
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<PIMSDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<PIMSDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                services.AddControllers(); 
            });

            builder.Configure(app =>
            {
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();  
                });
            });
        }
        
    }

    public class SearchIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;

        public SearchIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            _factory = factory;
        }

        [Theory]
        [InlineData("/search-pdf?Title=Document1")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<PIMSDbContext>();
                db.PdfDocuments.AddRange(
                    new PdfDocument { Id = 1, Title = "Document1", Author = "Author1", Publisher = "Publisher1", Year = 2020, Keywords = "Keyword1", DocumentType = "Type1" },
                    new PdfDocument { Id = 2, Title = "Document2", Author = "Author2", Publisher = "Publisher2", Year = 2021, Keywords = "Keyword2", DocumentType = "Type2" }
                );
                await db.SaveChangesAsync();
            }


            
            var response = await _client.GetAsync(url);

            
            response.EnsureSuccessStatusCode(); 
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Document1", responseString);
        }
    }
}
