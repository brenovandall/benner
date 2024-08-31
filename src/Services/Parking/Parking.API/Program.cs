var builder = WebApplication.CreateBuilder(args);

// Serviços para o container
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

// Configurações da pipeline
app.UseApiServices()
   .UseMigration();

app.Run();
