using Domain.Models;
using Domain.Responses;
using MediatR;

namespace Domain.Commands
{
    public record InsertWerknemersCommand(List<Werknemer> Werknemers) : IRequest<Dictionary<int, WerfWerknemerInsertedResponse>>;
}
