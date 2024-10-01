using Domain.Commands;
using Domain.DataAccess;
using MediatR;

namespace API.Handlers
{
    public class DeleteWerfCommandHandler : IRequestHandler<DeleteWerfCommand>
    {
        private readonly BuildSoftwareDbContext _context;

        public DeleteWerfCommandHandler(BuildSoftwareDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteWerfCommand request, CancellationToken cancellationToken)
        {
            var werf = await _context.Werven.FindAsync(request.Id, cancellationToken);
            if (werf == null)
            {
                throw new ArgumentException($"WerfId {@request.Id} bestaat niet.");
            }

            _context.Werven.Remove(werf);
            _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
