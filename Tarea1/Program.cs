using Microsoft.EntityFrameworkCore;
using Tarea1.Components;
using Tarea1.DAL;
using Tarea1.Services;
using Microsoft.AspNetCore.Routing.Constraints;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var ConStr = builder.Configuration.GetConnectionString("SqlConStr");
builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlServer(ConStr));

//Inyeccion del Service
builder.Services.AddScoped<TecnicosService>();
builder.Services.AddScoped<ClientesService>();
builder.Services.AddScoped<CiudadesServices>();
builder.Services.AddScoped<TicketsService>();
builder.Services.AddScoped<SistemasService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => { options.DetailedErrors = true; }); //
