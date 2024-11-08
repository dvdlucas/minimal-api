using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominios.DTOs;
using minimal_api.Dominios.Entidades;
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
builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();

var app = builder.Build();
#endregion

# region Home
app.MapGet("/", () => Results.Json(new Home { })).WithTags("Home");
#endregion

# region Administradores
app.MapPost("/administradores/login", ([FromBody] LoginDTO loginDTO, IAdministradorServico administradorService) =>
{
    if (administradorService.Login(loginDTO) != null)
        return Results.Ok("Login com sucesso");
    else
        return Results.Unauthorized();
}).WithTags("Administradores");


app.MapGet("/administradores/BuscarTodos", ([FromQuery] int? pagina, IAdministradorServico administradorService) =>
{
    List<Administrador> administradores = new List<Administrador>();
    administradores = administradorService.Todos(pagina);
    return administradores;
}).WithTags("Administradores");

app.MapGet("/administradores/BuscarPorId/{id}", ([FromRoute] int id, IAdministradorServico administradorServico) =>
{
    Administrador administrador = administradorServico.BuscarPorId(id);
    if (administrador == null) return Results.NotFound();
    return Results.Ok(administrador);
}).WithTags("Administradores");

app.MapPut("/administradores/editar/{id}", ([FromBody] int id, AdministradorDTO administradorDto, IAdministradorServico administradorServico) =>
{

    Administrador administrador = administradorServico.BuscarPorId(id);
    if (administrador == null) return Results.NotFound();

    administrador.Email = administradorDto.Email;
    administrador.Perfil = administradorDto.Perfil;
    administrador.Senha = administradorDto.Senha;

    administradorServico.Editar(administrador);
    return Results.Ok(administrador);
}).WithTags("Administradores");

app.MapDelete("/administradores/delete/{id}", ([FromQuery] int id, IAdministradorServico administradorServico) =>
{
    Administrador administrador = administradorServico.BuscarPorId(id);
    administradorServico.Deletar(administrador);
    return Results.Ok("Excluido");
}).WithTags("Administradores");
#endregion

#region Veiculos

app.MapPost("/veiculos", ([FromBody] VeiculoDTO veiculoDto, IVeiculoServico veiculoServico) =>
{
    var validacao = new ErrosDeValidacao();

    if (string.IsNullOrEmpty(veiculoDto.Nome))
    {
        validacao.Mensagens.Add("Erro, Faltando o nome");
    };

    if (string.IsNullOrEmpty(veiculoDto.Marca))
    {
        validacao.Mensagens.Add("Erro, Faltando a marca");
    };
    if(validacao.Mensagens.Count > 0)
    {
        Results.BadRequest(validacao);
    }
    var veiculo = new Veiculo
     {
        Nome = veiculoDto.Nome,
        Marca = veiculoDto.Marca,
        Ano = veiculoDto.Ano
     };
        veiculoServico.Incluir(veiculo);
        return Results.Created("/veiculo/{veiculo.Id}", veiculo);
}).WithTags("Veiculos");

app.MapGet("/veiculos/BuscarTodos", ([FromQuery] int? pagina, IVeiculoServico veiculoServico ) =>
{
    List<Veiculo> veiculos = new List<Veiculo>();
    veiculos = veiculoServico.Todos(pagina);
    return veiculos;
}).WithTags("Veiculos");

app.MapGet("/veiculos/BuscarPorId/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
{
    Veiculo veiculo = veiculoServico.BuscarPorId(id);
    if (veiculo == null) return Results.NotFound();
    return Results.Ok(veiculo);
}).WithTags("Veiculos");

app.MapPut("/veiculos/editar/{id}", ([FromBody]int id, VeiculoDTO veiculoDto, IVeiculoServico veiculoServico) =>
{

    Veiculo veiculo = veiculoServico.BuscarPorId(id);
    if (veiculo == null) return Results.NotFound();

    veiculo.Nome = veiculoDto.Nome;
    veiculo.Marca = veiculoDto.Marca;
    veiculo.Ano = veiculoDto.Ano;
   
    veiculoServico.Atualizar(veiculo);
    return Results.Ok(veiculo);
}).WithTags("Veiculos");

app.MapDelete("/veiculos/delete/{id}", ([FromQuery]  int id, IVeiculoServico veiculoServico) =>
{
    Veiculo veiculo = veiculoServico.BuscarPorId(id);
    veiculoServico.Apagar(veiculo);
    return Results.Ok("Excluido");
}).WithTags("Veiculos");
#endregion

#region App
app.UseSwagger();
app.UseSwaggerUI();

app.Run();

#endregion
