using Domain.Models;
using MediatR;

namespace Domain.Queries
{
    public record GetWerfByIdQuery(int Id) : IRequest<Werf>;
}
