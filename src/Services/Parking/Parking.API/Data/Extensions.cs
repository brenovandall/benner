namespace Parking.API.Data;

public static class Extensions
{
    /// <summary>
    /// Método para criar as migrações automaticamente e etc
    /// </summary>
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<ParkingContext>();

        // Aplica as migrações pendentes no banco de dados
        dbContext.Database.MigrateAsync();

        return app;
    }
}
