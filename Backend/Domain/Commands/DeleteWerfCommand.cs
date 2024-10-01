using MediatR;

namespace Domain.Commands
{
    public record DeleteWerfCommand(int Id) : IRequest;
}
