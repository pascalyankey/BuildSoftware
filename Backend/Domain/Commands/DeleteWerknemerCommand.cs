using MediatR;

namespace Domain.Commands
{
    public record DeleteWerknemerCommand(int Id) : IRequest;
}
