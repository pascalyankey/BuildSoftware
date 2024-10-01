using Domain.Models;
using MediatR;

namespace Domain.Queries
{
    public record GetWervenListQuery : IRequest<List<Werf>>;
}
