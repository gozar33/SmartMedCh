using Microsoft.EntityFrameworkCore;
using SmartMed.API.ErrorHandling;
using SmartMed.Application;
using SmartMed.Application.Medications;
using SmartMed.Domain.Medications;
using SmartMed.Infrastructure;
using SmartMed.Infrastructure.Persistence;
using SmartMed.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureInfrastructureLayer(builder.Configuration);
builder.Services.ConfigureApplicationLayer(builder.Configuration);
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ValidateModelAttribute());
})
              .ConfigureApiBehaviorOptions(options =>
              {
                  options.SuppressModelStateInvalidFilter = true;

              });

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<GlobalExceptionHandler>();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
