using DailyExpenses.Api.Extensions;
using DailyExpenses.Application;
using DailyExpenses.Infrastructure;
using DailyExpenses.Infrastructure.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationApi()
    .AddApplication()
    .AddApplicationInfrastructure(builder.Configuration)
    .AddControllers().Services
    .AddEndpointsApiExplorer()
    .AddApplicationSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

MigrateDatabase(app);
AppDbInitializer.Seed(app);

app.MapControllers();

app.Run();

static void MigrateDatabase(IHost host)
{
    using var scope = host.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.Migrate();
}
