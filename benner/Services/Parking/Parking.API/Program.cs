var builder = WebApplication.CreateBuilder(args);

// Servi�os para o container
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

// Configura��es da pipeline
app.UseApiServices()
   .UseMigration();

app.Run();
