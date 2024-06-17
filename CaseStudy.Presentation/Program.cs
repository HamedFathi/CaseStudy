using CaseStudy.Application.Extensions;
using CaseStudy.Infrastructure;
using CaseStudy.Infrastructure.Extensions;
using HamedStack.AspNetCore.Endpoint;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMinimalApiEndpoints();

builder.AddServiceDefaults(); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "RedisCache_";
});

builder.Services.AddInfrastructure<CaseStudyContext>(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.SigningKey = "/H4A/2ZLnGPh3WpVAjoFW9BWfQd7G3vpVSC+e27Qyk4ACKNK65UWlXUwQs8xq/s+";
    opt.ValidAudience = "https://example.com/";
    opt.ValidIssuer = "https://example.com/";
});

builder.Services.AddDbContext<CaseStudyContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CaseStudyDb") ?? "Data Source=casestudydb.db"));


builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapMinimalApiEndpoints();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<CaseStudyContext>();
        context.Database.Migrate();
        context.Database.EnsureCreated();
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<CaseStudy.Presentation.Program>>();
        logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
}

app.MapDefaultEndpoints();

app.Run();

namespace CaseStudy.Presentation
{
    public partial class Program
    {
    }
}