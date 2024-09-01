using Parking.UI.Models;
using Refit;

namespace Parking.UI.Services;

/// <summary>
/// Interface que define os métodos para a API
/// Utiliza refit para facilitar a chamada
/// </summary>
public interface IVeiculoService
{
    [Get("/veiculo")]
    Task<GetVeiculosResponse> GetVeiculos();

    [Get("/veiculo/{id}")]
    Task<GetVeiculoByIdResponse> GetVeiculoById(Guid id);

    [Post("/veiculo")]
    Task<CreateVeiculoResponse> CreateVeiculo(CreateVeiculoRequest request);
}
