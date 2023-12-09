using BankTradingService.Application.Commands;
using BankTradingService.Application.DTOs;
using BankTradingService.Data.Context;
using BankTradingService.Data.Context.Interface;
using BankTradingService.Data.Repositories;
using BankTradingService.Data.Repositories.Interface;
using BankTradingService.Shared.Enums;
using BankTradingService.Shared.Messaging;
using BankTradingService.Shared.Utilities.Interface;
using BankTradingService.Shared.Utilities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddSingleton<ILoggerFactory, LoggerFactory>();
builder.Services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));


builder.Services.AddDbContext<TradeDbContext>();

builder.Services.AddScoped<ITradeDbContext, TradeDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<IUserTradeRepository, UserTradeRepository>();

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(TradeActivityDTO).Assembly);
    cfg.RegisterServicesFromAssemblyContaining<Program>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
