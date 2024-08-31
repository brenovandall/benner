using Microsoft.EntityFrameworkCore;

namespace Parking.gRPC.Data;

public static class Extensions
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<ParkingContext>();

        dbContext.Database.MigrateAsync();

        return app;
    }
}
