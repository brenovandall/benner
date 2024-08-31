using Microsoft.EntityFrameworkCore;
using Parking.API.Models;

namespace Parking.API.Data;

public class ParkingContext : DbContext
{
    public DbSet<Estacionamento> Estacionamentos { get; set; }
    public DbSet<Veiculo> Veiculos { get; set; }

    public ParkingContext(DbContextOptions<ParkingContext> options) 
        : base(options)
    {
    }
}
