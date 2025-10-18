using Blazored.LocalStorage;
using PicasYFamas.BlazorApp.Components;
using PicasYFamas.BlazorApp.Components.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configurar HttpClient para la API
builder.Services.AddHttpClient<IGameApiService, GameApiService>(client =>
{
    // Cambiar a la URL de tu API Backend
    client.BaseAddress = new Uri("https://localhost:7204/");
    client.Timeout = TimeSpan.FromSeconds(30);
});

// LocalStorage para guardar datos del jugador
builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();