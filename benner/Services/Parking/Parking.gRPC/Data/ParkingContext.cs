using Microsoft.EntityFrameworkCore;
using Parking.gRPC.Models;

namespace Parking.gRPC.Data;

public class ParkingContext : DbContext
{
    public DbSet<PrecoBase> PrecosBase { get; set; }

    public ParkingContext(DbContextOptions<ParkingContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PrecoBase>().HasData(
            new PrecoBase
            {
                Id = Guid.NewGuid(),
                DataInicio = new DateTime(2024, 1, 1),
                DataFim = new DateTime(2024, 12, 31),
                ValorHoraInicial = 2.00m,
                ValorHoraAdicional = 1.00m
            },
            new PrecoBase
            {
                Id = Guid.NewGuid(),
                DataInicio = new DateTime(2023, 1, 1),
                DataFim = new DateTime(2023, 12, 31),
                ValorHoraInicial = 1.50m,
                ValorHoraAdicional = 0.75m
            }
        );
    }
}
