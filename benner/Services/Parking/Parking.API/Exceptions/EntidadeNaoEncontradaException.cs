using BuildingBlocks.Exceptions;

namespace Parking.API.Exceptions;

public class EntidadeNaoEncontradaException : NotFoundException
{
    public EntidadeNaoEncontradaException(Guid Id) : base("Entidade", Id) { }
}
