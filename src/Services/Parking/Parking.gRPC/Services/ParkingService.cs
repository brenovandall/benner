using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Parking.gRPC.Data;
using Parking.gRPC.Models;

namespace Parking.gRPC.Services;

/// <summary>
/// Serviço gRPC capaz de calcular os valores necessários no estacionamento
/// </summary>
public class ParkingService (ParkingContext dbContext)
    : ParkingProtoService.ParkingProtoServiceBase
{
    /// <summary>
    /// Método que calcula o valor final conforme a vigência do carro
    /// </summary>
    public override async Task<CalcularValorResponse> CalcularValorFinal(CalcularValorRequest request, ServerCallContext context)
    {
        // Converte as datas a partir das strings da requisição
        var dataEntrada = DateTime.Parse(request.DataEntrada);
        var dataSaida = DateTime.Parse(request.DataSaida);
        var duracao = dataSaida - dataEntrada;

        // Obter o preço aplicável a data de entrada
        var precoBase = await dbContext.PrecosBase
            .Where(x => x.DataInicio <= dataEntrada && x.DataFim >= dataEntrada)
            .OrderByDescending(x => x.DataInicio)
            .FirstOrDefaultAsync();

        // Caso não houver nenhum preço base para data de entrada, lança uma exceção
        if (precoBase is null)
            throw new RpcException(new Status(StatusCode.NotFound, "Tabela de preços não encontrada para a data de entrada informada."));

        double valorFinal = CalcularValor(duracao, precoBase);

        // Retorna um valor final para o client
        return new CalcularValorResponse { ValorFinal = valorFinal };
    }

    /// <summary>
    /// Método para auxiliar o calculo do valor final do estacionamento
    /// </summary>
    private double CalcularValor(TimeSpan duracao, PrecoBase precoBase)
    {
        // Se a duração for de 30 minutos ou menos, cobra metade do valor da primeira hora
        if (duracao.TotalMinutes <= 30)
        {
            return (double)(precoBase.ValorHoraInicial / 2);
        }

        double valorFinal = (double)precoBase.ValorHoraInicial;

        // Calcula os minutos adicionais
        var minutosAdicionais = duracao.TotalMinutes - 60;

        if (minutosAdicionais > 0)
        {
            // Considera os 10 minutos de tolerancia
            var horasAdicionais = Math.Ceiling((minutosAdicionais - 10) / 60);

            if (horasAdicionais < 0) horasAdicionais = 0; // Garantir que o valor nao seja negativo

            valorFinal += (double)(horasAdicionais * (double)precoBase.ValorHoraAdicional);
        }

        // Retorna o valor final
        return valorFinal;
    }
}
