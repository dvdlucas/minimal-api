using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominios.DTOs;
using minimal_api.Dominios.Infraestrutura.Db;
using minimal_api.Dominios.Interfaces;
using minimal_api.Dominios.ModelViews;
using minimal_api.Dominios.Servicos;

# region Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContexto>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("mysql");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();

var app = builder.Build();
#endregion

# region Home
app.MapGet("/", () => Results.Json(new Home { }));
#endregion

# region Administradores
app.MapPost("/administradores/login", ([FromBody] LoginDTO loginDTO, IAdministradorServico administrador) =>
{
    if (administrador.Login(loginDTO) != null)
        return Results.Ok("Login com sucesso");
    else
        return Results.Unauthorized();
});
#endregion

#region Veiculos
app.MapPost("/veiculos", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{
    if (veiculoServico.Incluir(veiculoDTO) != null)
        return Results.Ok("Cadastro Realizado com sucesso");
    else
        return Results.Unauthorized();
});
#endregion

#region App
app.UseSwagger();
app.UseSwaggerUI();

app.Run();

#endregion
