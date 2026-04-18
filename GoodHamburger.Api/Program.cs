using GoodHamburger.Api.Services;
using GoodHamburger.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar o serviço de pedidos como Singleton para manter os dados em memória durante a execução
// Register EF Core DbContext using SQL Server for local development
var connectionString = builder.Configuration.GetConnectionString("GoodHamburgerConnection")
                      ?? "Server=(localdb)\\mssqllocaldb;Database=GoodHamburgerDb;Trusted_Connection=True;MultipleActiveResultSets=true";

builder.Services.AddDbContext<OrdersDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Register OrderService to use the DbContext (scoped)
builder.Services.AddScoped<IOrderService, OrderService>();

// Habilitar CORS para o frontend Blazor
var frontendOrigins = new[]
{
    "http://localhost:5267", // frontend http profile
    "https://localhost:7275", // frontend https profile
    "http://localhost:37974", // frontend iisExpress http
    "http://localhost:5078", // api (useful for local testing)
    "https://localhost:7286" // api https profile
};

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins(frontendOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Ensure database is created and seeded at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Redirect HTTP to HTTPS when possible
app.UseHttpsRedirection();

// Use the named CORS policy so browser requests from the frontend are allowed
app.UseCors("FrontendPolicy");

app.UseAuthorization();
app.MapControllers();

app.Run();
