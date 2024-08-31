namespace Parking.gRPC.Models;

public class PrecoBase
{
    public Guid Id { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public decimal ValorHoraInicial { get; set; }
    public decimal ValorHoraAdicional { get; set; }
}
