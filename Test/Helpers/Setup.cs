using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;  // For WebApplicationFactory
using MinimalApi.Dominio.Interfaces;
using Test.Mocks;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Test.Helpers
{
    public class Setup
    {
        public const string PORT = "5001";
        public static TestContext TestContext = default!;
        public static WebApplicationFactory<Startup> WebAppFactory = default!;
        public static HttpClient Client = default!;

        public static void ClassInit(TestContext testContext)
        {
            // Initialize the test context
            TestContext = testContext;

            // Create WebApplicationFactory with necessary configurations
            WebAppFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    // Set environment and other settings
                    builder.UseSetting("https_port", PORT)
                           .UseEnvironment("Testing");

                    // Register mock services here
                    builder.ConfigureServices(services =>
                    {
                        services.AddScoped<IAdministradorServico, AdministradorServicoMock>();
                    });
                });

            // Create HttpClient to use for sending requests in tests
            Client = WebAppFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                // Set the base address for the test server
                BaseAddress = new Uri($"https://localhost:{PORT}")
            });
        }

        public static void ClassCleanup()
        {
            // Dispose of the HttpClient and WebApplicationFactory
            Client?.Dispose();
            WebAppFactory?.Dispose();
        }
    }
}
