using BtgSimuladorCredito.Application.Services;
using Microsoft.EntityFrameworkCore;
using BtgSimuladorCredito.Infrastructure.Data;
using Microsoft.Extensions.Options;
using BtgSimuladorCredito.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();
builder.Services.AddScoped<SimuladorCreditoService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseInMemoryDatabase("BtgSimuladorDb"));
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.Run();
