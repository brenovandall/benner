using Microsoft.EntityFrameworkCore;
using Parking.API.Data;
using Parking.gRPC;

namespace Parking.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Injeção de dependencia do Carter (facilitador na criação de minimal APIs)
        services.AddCarter();

        // Configuração do banco de dados
        services.AddDbContext<ParkingContext>(options => 
            options.UseSqlite(configuration.GetConnectionString("DB_CONNECTION")));

        // Referencias do assembly para injetar o MediatR
        var assembly = typeof(Program).Assembly;
        var validation = typeof(ValidationBehaviors<,>);
        var logging = typeof(LoggingBehavior<,>);

        // Configura o MediatR
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(validation);
            config.AddOpenBehavior(logging);
        });

        // Adiciona os validadores usando Fluent
        services.AddValidatorsFromAssembly(assembly);

        // Configura o cliente gRPC para o serviço ParkingProtoService.
        services.AddGrpcClient<ParkingProtoService.ParkingProtoServiceClient>(options =>
        {
            options.Address = new Uri(configuration["GrpcSettings:ParkingUrl"]!);
        })
        // Manipulador para remover validação de vertificados
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            return handler;
        });

        services.AddExceptionHandler<CustomExceptionHandler>();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();

        // Middleware para tratamento global de exceções
        app.UseExceptionHandler(options => { });

        return app;
    }
}
