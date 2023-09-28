//Підключаємо глобально EntityFrameworkCore, щоб не звертатись до нього кожного разу і папку сервісів і контексту бази даних
global using Microsoft.EntityFrameworkCore;
using ABP.ua.Services;
using ABP.ua.Data;
var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Підключаємо створений сервіс
builder.Services.AddScoped<IUserService, UserService>();

//Підключаємо контекст бази даних
builder.Services.AddDbContext<DataContext>();

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
