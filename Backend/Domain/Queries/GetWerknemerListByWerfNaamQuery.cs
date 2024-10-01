using Domain.Models;
using MediatR;

namespace Domain.Queries
{
    public record GetWerknemerListByWerfNaamQuery(string WerfNaam) : IRequest<List<Werknemer>>;
}
