using Domain.Models;
using MediatR;

namespace Domain.Queries
{
    public record GetWerknemerListQuery : IRequest<List<Werknemer>>;
}
