using BusinessLogic;
using BusinessLogic.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Model.bounderies;
using Model.Entities;
using Model.Mappers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("serilogsettings.json");

// Add services to the container.
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddAutoMapper(typeof(MappersAssemble).Assembly);
IRepository<Player> playersRepository = RepositoryFactory.CreateRepository<Player>(builder.Configuration);
IRepository<GameSession> gameSessionRepository = RepositoryFactory.CreateRepository<GameSession>(builder.Configuration);
builder.Services.AddSingleton(playersRepository);
builder.Services.AddSingleton(gameSessionRepository);
builder.Services.AddSingleton< IPlayerService,PlayerService>();
builder.Services.AddSingleton< GameService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();
 
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints( endpoints =>
{
    endpoints.MapControllers();
});


app.Run();
// Make the implicit Program class public so test projects can access it
public partial class Program { }