using MediatR;
using System.Reflection;
using WebAPI.Extensions;
using Application.Extensions;
using Infrastructure.Extensions;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.RegisterGrapQLDependencies();
builder.Services.RegisterApplicationDependencies();
builder.Services.RegisterInfrastructureDependencies();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<CleanArchitectureDBContext>();
    dataContext.Database.Migrate();
}
app.UseHttpsRedirection();
app.MapControllers();
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapGraphQL(); });
app.Run();