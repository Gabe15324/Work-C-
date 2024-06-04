using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(
    options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

builder.Services.AddDbContext<BancoDeDados>();


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();


//https://localhost:5096/swagger/index.html

app.MapGet("/", () => "Loja de Games com EF + Swagger");

app.MapClienteAPI();
app.MapComprasAPI();
app.MapVendasAPI();
app.MapGamesAPI();

app.Run();
