using Microsoft.EntityFrameworkCore;
using WelderProjectManagement.Contexts;
using WelderProjectManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ProjectService>();
builder.Services.AddControllers()
.AddJsonOptions(ops =>
{
    ops.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});
builder.Services.AddDbContext<WelderProjectManagementContext>(ops => ops.UseSqlite("Data Source=./Database/welder_project_management.db"));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
