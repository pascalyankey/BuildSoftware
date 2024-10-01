using API.Models;
using Domain.Commands;
using Domain.DataAccess;
using Domain.Enums;
using Domain.Models;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Domain.Handlers
{
    public class InsertWerknemerCommandHandler : IRequestHandler<InsertWerknemersCommand, Dictionary<int, WerfWerknemerInsertedResponse>>
    {
        private readonly BuildSoftwareDbContext _context;
        private readonly JWTSettings _jwtSettings;

        public InsertWerknemerCommandHandler(BuildSoftwareDbContext context, IOptions<JWTSettings> jwtsettings)
        {
            _context = context;
            _jwtSettings = jwtsettings.Value;
        }

        public async Task<Dictionary<int, WerfWerknemerInsertedResponse>> Handle(InsertWerknemersCommand requests, CancellationToken cancellationToken)
        {
            var werknemers = new Dictionary<int, WerfWerknemerInsertedResponse>();
            var key = 0;

            foreach (var request in requests.Werknemers)
            {
                var werknemerInsertedResponse = new WerfWerknemerInsertedResponse();
                werknemerInsertedResponse.Voornaam = request.Voornaam;
                werknemerInsertedResponse.Achternaam = request.Achternaam;

                var werf = await _context.Werven.FindAsync(request.WerfId, cancellationToken);
                if (werf == null)
                {
                    werknemerInsertedResponse.IsSuccess = false;
                    werknemers.Add(key, werknemerInsertedResponse);
                }
                else
                {
                    werknemerInsertedResponse.WerfId = werf.Id;
                    werknemerInsertedResponse.WerfNaam = werf.Naam;

                    var rolBestaat = await _context.Rollen.AnyAsync(r => r.RolId == request.RolId, cancellationToken);
                    if (!rolBestaat)
                    {
                        var rol = new Rol { RolId = request.RolId, RolBeschrijving = Enum.GetName(typeof(RolType), request.RolId), Werknemers = null! };
                        await _context.Rollen.AddAsync(rol, cancellationToken);
                        await _context.SaveChangesAsync();
                    }

                    var bestaatReeds = await _context.Werknemers.AnyAsync(w =>
                                                    w.Voornaam == request.Voornaam &&
                                                    w.Achternaam == request.Achternaam &&
                                                    w.WerfId == request.WerfId &&
                                                    w.Rol == request.Rol,
                                                    cancellationToken);
                    if (bestaatReeds)
                    {
                        werknemerInsertedResponse.Error = $"Werknemer {request.Voornaam} {request.Achternaam} bestaat reeds in {werf.Naam}";
                    }
                    else
                    {
                        if (request.Werf ==  null)
                        {
                            request.Werf = werf;
                        }

                        if (request.Rol == null)
                        {
                            request.Rol = await _context.Rollen.FindAsync(request.RolId, cancellationToken);
                        }

                        await _context.Werknemers.AddAsync(request, cancellationToken);

                        try
                        {
                            await _context.SaveChangesAsync(cancellationToken);
                        }
                        catch (Exception ex)
                        {
                            werknemerInsertedResponse.IsSuccess = false;
                            werknemerInsertedResponse.Error = ex.Message;
                        }

                        werknemerInsertedResponse.IsSuccess = true;
                        werknemers.Add(key, werknemerInsertedResponse);
                    }
                }

                key++;
            }

            return werknemers;
        }
    }
}
