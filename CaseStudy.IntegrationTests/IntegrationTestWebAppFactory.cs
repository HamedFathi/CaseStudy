using System.Data.Common;
using CaseStudy.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CaseStudy.IntegrationTests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<CaseStudy.Presentation.Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<CaseStudyContext>), typeof(DbConnection));

            services.AddSingleton<DbConnection>(container =>
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();

                return connection;
            });

            services.AddDbContext<CaseStudyContext>((container, options) =>
            {
                var connection = container.GetRequiredService<DbConnection>();
                options.UseSqlite(connection);
            });

            using var scope = services.BuildServiceProvider().CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<CaseStudyContext>();

            // db.Database.EnsureDeleted(); // For SQLite in-memory is not necessary.
            db.Database.EnsureCreated();
            SeedData.Initialize(scope.ServiceProvider);

            builder.UseEnvironment("Development");
        });
    }
}