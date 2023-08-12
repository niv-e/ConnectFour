using BusinessLogic.BoardCheck;
using BusinessLogic.Contracts;
using BusinessLogic;
using BusinessLogic.Model.Mappers;
using DAL.Contracts;
using DAL;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ConnectFourWebApplication.Pages.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Configuration.AddJsonFile("serilogsettings.json");
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddMemoryCache();
builder.Services.AddLogging();
builder.Services.AddAutoMapper(typeof(MappersAssemble).Assembly);
builder.Services.AddHttpClient("DefaultClient",client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("applicationUrl"));
});

AddCustomServices(builder);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();
static void AddCustomServices(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<ConnectFourDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });


    builder.Services.AddScoped<IGameSessionRepository, GameSessionRepository>();
    builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
    builder.Services.AddScoped<IQueriesRepository, QueriesRepository>();
    builder.Services.AddScoped<IPlayerService, PlayerService>();
    builder.Services.AddScoped<IGameService, GameService>();
    builder.Services.AddScoped<IBoardChecker, BoardChecker>();
    builder.Services.AddSingleton<_DataHolder>();
}