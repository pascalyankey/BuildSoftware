using Domain.DataAccess;
using Domain.Models;
using Domain.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers
{
    public class GetWerknemerListHandler : IRequestHandler<GetWerknemerListQuery, List<Werknemer>>
    {
        private readonly BuildSoftwareDbContext _context;

        public GetWerknemerListHandler(BuildSoftwareDbContext context)
        {
            _context = context;
        }

        public async Task<List<Werknemer>> Handle(GetWerknemerListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Werknemers.ToListAsync(cancellationToken);
        }
    }
}
