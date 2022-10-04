using Notify.Strategies;

using StrategyDesignPattern.ConcreteStrategies;
using StrategyDesignPattern.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register dependencies
builder.Services.AddTransient<INotificationStrategy, EmailNotificationStrategy>();
builder.Services.AddTransient<INotificationStrategy, SMSNotificationStrategy>();
builder.Services.AddTransient<INotificationContext, NotificationContext>();

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
