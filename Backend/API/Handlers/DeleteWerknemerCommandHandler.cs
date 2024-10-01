using API.Exceptions;
using Domain.Commands;
using Domain.DataAccess;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers
{
    public class DeleteWerknemerCommandHandler : IRequestHandler<DeleteWerknemerCommand>
    {
        private readonly BuildSoftwareDbContext _context;

        public DeleteWerknemerCommandHandler(BuildSoftwareDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteWerknemerCommand request, CancellationToken cancellationToken)
        {
            var werknemer = await _context.Werknemers.FindAsync(request.Id, cancellationToken);
            if (werknemer == null)
            {
                throw new NotFoundException($"WerknemerId {@request.Id} bestaat niet.");
            }

            var isActiefInWerf = await _context.Werven.AnyAsync(w => w.Id == werknemer.WerfId && w.Status != Status.Afgerond, cancellationToken);
            if (isActiefInWerf)
            {
                throw new Exception("Werknemer is actief in een werf");
            }

            _context.Werknemers.Remove(werknemer);
            _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
