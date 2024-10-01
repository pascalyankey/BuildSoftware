using Domain.DataAccess;
using Domain.Models;
using Domain.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Handlers
{
    public class GetWerknemerListByWerfNaamHandler : IRequestHandler<GetWerknemerListByWerfNaamQuery, List<Werknemer>>
    {
        private readonly BuildSoftwareDbContext _context;

        public GetWerknemerListByWerfNaamHandler(BuildSoftwareDbContext context)
        {
            _context = context;
        }

        public async Task<List<Werknemer>> Handle(GetWerknemerListByWerfNaamQuery request, CancellationToken cancellationToken)
        {
            return await _context.Werven
                 .Where(w => w.Naam.Contains(request.WerfNaam))
                 .SelectMany(x => x.Werknemers)
                 .ToListAsync(cancellationToken);
        }
    }
}
