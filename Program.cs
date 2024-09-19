using Microsoft.EntityFrameworkCore;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddDbContext<DbContexto>(options => options.UseMySql(
    builder.Configuration.GetConnectionString("mysql"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
));

app.MapGet("/", () => "Hello!");

app.MapPost("/login", (LoginDTO LoginDTO) => {
    if (LoginDTO.Email == "adm@test.com" && LoginDTO.Senha == "123456") {
        return Results.Ok("Login efetuado com sucesso!");
    }
    else 
        return Results.Unauthorized();
});

app.Run();