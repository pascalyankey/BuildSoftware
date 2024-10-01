using API.Dto;
using AutoMapper;
using Domain.Commands;
using Domain.DataAccess;
using Domain.Models;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Handlers
{
    public class InsertWerfCommandHandler : IRequestHandler<InsertWerfCommand, Dictionary<int, WerfWerknemerInsertedResponse>>
    {
        private readonly BuildSoftwareDbContext _context;
        private readonly IMediator _mediator;

        public InsertWerfCommandHandler(BuildSoftwareDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Dictionary<int, WerfWerknemerInsertedResponse>> Handle(InsertWerfCommand request, CancellationToken cancellationToken)
        {
            var bestaatReeds = await _context.Werven.AnyAsync(w => w.Naam == request.Naam);
            if (bestaatReeds)
            {
                throw new BadHttpRequestException("Deze werf bestaat reeds.");
            }

            var werf = new Werf 
            { 
                Naam = request.Naam,
                StartDatum = request.StartDatum,
                EindDatum = request.EindDatum,
                Status = request.Status
            };

            await _context.Werven.AddAsync(werf, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            Dictionary<int, WerfWerknemerInsertedResponse> result = null!;

            if (request.Werknemers.Count > 0)
            {
                request.Werknemers.ForEach(w => w.WerfId = werf.Id);
                result = await _mediator.Send(new InsertWerknemersCommand(request.Werknemers));
            }

            return result!;
        }
    }
}
