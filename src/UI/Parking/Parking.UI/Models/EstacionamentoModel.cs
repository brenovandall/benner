using System.ComponentModel.DataAnnotations;

namespace Parking.UI.Models;

public class Estacionamento
{
    public DateTime DataEntrada { get; set; }
    public DateTime? DataSaida { get; set; }
    public decimal? ValorFinal { get; set; }
    public TimeSpan? Duracao { get; set; }
    public Guid VeiculoId { get; set; }
    public Guid Id { get; set; }
}

public class EstacionamentoModel
{
    [Required(ErrorMessage = "A data de entrada não pode ser nula")]
    public DateTime DataEntrada { get; set; }
    public DateTime? DataSaida { get; set; }
    public decimal? ValorFinal { get; set; }
    public TimeSpan? Duracao { get; set; }
    [Required(ErrorMessage = "O veículo deve ser selecionado")]
    public Guid VeiculoId { get; set; }
}

public class GetEstacionamentosResponse
{
    public IReadOnlyList<Estacionamento> Estacionamentos { get; set; }
}

public class GetEstacionamentoByIdResponse
{
    public Estacionamento Estacionamento { get; set; }
}

public class CreateEstacionamentoResponse
{
    public Guid Id { get; set; }
}

public class UpdateEstacionamentoResponse
{
    public bool IsSuccess { get; set; }
}

public record CreateEstacionamentoRequest(DateTime DataEntrada, DateTime? DataSaida, decimal? ValorFinal, TimeSpan? Duracao, Guid VeiculoId);
public record UpdateEstacionamentoRequest(Guid Id, DateTime DataEntrada, DateTime? DataSaida, decimal? ValorFinal, TimeSpan? Duracao, Guid VeiculoId);