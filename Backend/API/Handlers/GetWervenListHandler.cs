using Domain.DataAccess;
using Domain.Models;
using Domain.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Handlers
{
    public class GetWervenListHandler : IRequestHandler<GetWervenListQuery, List<Werf>>
    {
        private readonly BuildSoftwareDbContext _context;

        public GetWervenListHandler(BuildSoftwareDbContext context)
        {
            _context = context;
        }

        public async Task<List<Werf>> Handle(GetWervenListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Werven
                .Include(w => w.Werknemers)
                .Where(w => w.StartDatum.Date < DateTime.Now.Date && w.Status == Enums.Status.InWerking)
                .ToListAsync(cancellationToken);
        }
    }
}
