using ETL.Abstractions.Interfaces;
using ETL.API.Hubs;
using ETL.Core.Extensions;
using ETL.Core.Services;
using ETL.Database;
using ETL.Database.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Register repositories
builder.Services.AddScoped<IPipelineConfigRepository, PipelineConfigRepository>();
builder.Services.AddScoped<IPipelineLogRepository, PipelineLogRepository>();

builder.Services.AddETLCore(builder.Configuration);

// Add API-specific services
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddSwaggerGen();

// Register DbContext
builder.Services.AddDbContext<ETLDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ETLDatabase")));

var app = builder.Build();

// Initialize scheduler
using (var scope = app.Services.CreateScope())
{
    var scheduler = scope.ServiceProvider.GetRequiredService<SchedulerService>();
    await scheduler.ScheduleAllPipelinesAsync();
}

app.MapControllers();
app.MapHub<ETLHub>("/etlhub");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


