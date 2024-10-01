using API.Exceptions;
using Domain.DataAccess;
using Domain.Models;
using Domain.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers
{
    public class GetWerfByIdHandler : IRequestHandler<GetWerfByIdQuery, Werf>
    {
        private readonly BuildSoftwareDbContext _context;

        public GetWerfByIdHandler(BuildSoftwareDbContext context)
        {
            _context = context;
        }

        public async Task<Werf> Handle(GetWerfByIdQuery request, CancellationToken cancellationToken)
        {
            var werf = await _context.Werven
                .Include(w => w.Werknemers)
                .Where(w => w.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (werf == null) 
            {
                throw new NotFoundException($"WerfId {request.Id} bestaat niet.");
            }

            return werf;
        }
    }
}
