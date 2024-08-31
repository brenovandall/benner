using Microsoft.EntityFrameworkCore;
using Parking.gRPC.Data;
using Parking.gRPC.Services;

var builder = WebApplication.CreateBuilder(args);

// Serviços para o container
builder.Services.AddGrpc();

builder.Services.AddDbContext<ParkingContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DB_CONNECTION")));

var app = builder.Build();

// Configurações da pipeline
app.UseMigration();

app.MapGrpcService<ParkingService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
