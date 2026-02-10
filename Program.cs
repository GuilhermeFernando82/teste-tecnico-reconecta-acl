using Apivscode2;
using Apivscode2.Interfaces;
using Apivscode2.Repository;
using Apivscode2.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProducerRepository, ProducerRepository>();
builder.Services.AddScoped<ITerritoryRepository, TerritoryRepository>();
builder.Services.AddScoped<IProducerService, ProducerService>();
builder.Services.AddScoped<IFinancialService, FinancialService>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ApiUsers",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
