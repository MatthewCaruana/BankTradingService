using BankTradingService.Application.Services;
using BankTradingService.Application.Services.Interfaces;
using BankTradingService.Data.Context;
using BankTradingService.Data.Context.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TradeDbContext>();

builder.Services.AddScoped<ITradeDbContext, TradeDbContext>();

builder.Services.AddScoped<ITradeService, TradeService>();

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
