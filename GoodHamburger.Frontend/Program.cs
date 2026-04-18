using GoodHamburger.Frontend.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registrar HttpClient para chamadas de API
// Definir BaseAddress apontando para a API local (ajuste conforme o profile que você executar)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5078") });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
