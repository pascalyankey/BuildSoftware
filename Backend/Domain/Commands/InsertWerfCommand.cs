using Domain.Models;
using Domain.Responses;
using MediatR;

namespace Domain.Commands
{
    public record InsertWerfCommand(string Naam, DateTime StartDatum, DateTime EindDatum, Enums.Status Status, List<Werknemer> Werknemers) 
        : IRequest<Dictionary<int, WerfWerknemerInsertedResponse>>;
}
