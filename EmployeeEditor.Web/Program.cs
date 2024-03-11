using EmployeeEditor.Application.Abstractions;
using EmployeeEditor.Application.Abstractions.Data;
using EmployeeEditor.Application.Abstractions.Microservices;
using EmployeeEditor.Application.Behaviors;
using EmployeeEditor.Application.Employees.Create;
using EmployeeEditor.Application.Services;
using EmployeeEditor.Infrastructure.Data;
using EmployeeEditor.Infrastructure.Repositories;
using EmployeeEditor.Web.Controllers;
using EmployeeEditor.Web.Middleware;
using FluentValidation;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddSingleton<IConnectionMultiplexer>(s =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")));

builder.Services.AddMediatR(m => m.RegisterServicesFromAssemblies(
    typeof(Program).Assembly,
    typeof(UnitOfWorkBehavior<,>).Assembly,
    typeof(ForeachAwaitPublisher).Assembly,
    typeof(CreateEmployeeCommand).Assembly,
    typeof(CreateEmployeeCommandValidator).Assembly,
    typeof(CreateEmployeeCommandHandler).Assembly));

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddValidatorsFromAssemblyContaining<CreateEmployeeCommandValidator>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapEmployeeEndpoints();

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseMiddleware<RequestLogContextMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
