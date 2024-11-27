using Microsoft.EntityFrameworkCore;
using ProjectIko.Db;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}", optional: true);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddDbContextPool<ProjectIko.Db.AppContext>(options =>
{
    options.EnableSensitiveDataLogging(true);
    options.EnableDetailedErrors(true);
    options.UseNpgsql(builder.Configuration["Data:PGConnection:ConnectionString"]);
});

builder.Services.RegisterPGDataLayerServices();

builder.Services.AddControllers();

WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
