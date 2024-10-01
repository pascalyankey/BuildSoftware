using API.Exceptions;
using API.Models;
using API.Services;
using Domain.Commands;
using Domain.DataAccess;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;
using System.Web.Http;

namespace API.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, WerknemerToken>
    {
        private readonly BuildSoftwareDbContext _context;
        private readonly JWTSettings _jwtSettings;

        public LoginCommandHandler(BuildSoftwareDbContext context, IOptions<JWTSettings> jwtsettings)
        {
            _context = context;
            _jwtSettings = jwtsettings.Value;
        }

        public async Task<WerknemerToken> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var tokenService = new TokenService();
            var werknemer = await _context.Werknemers
                        .Where(u => u.Emailadres == request.Emailadres && u.Wachtwoord == request.Wachtwoord)
                        .FirstOrDefaultAsync();

            WerknemerToken werknemerToken = null;

            if (werknemer != null)
            {
                var refreshToken = tokenService.GenerateRefreshToken();
                werknemer.RefreshTokens.Add(refreshToken);
                await _context.SaveChangesAsync(cancellationToken);

                werknemerToken = new WerknemerToken();
                werknemerToken.Voornaam = werknemer.Voornaam;
                werknemerToken.Achternaam = werknemer.Achternaam;
                werknemerToken.RefreshToken = refreshToken.Token;
            }

            if (werknemerToken == null)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Ongeldige login" };
                throw new HttpResponseException(msg);
            }

            werknemerToken.AccessToken = tokenService.GenerateAccessToken(werknemer.Id, _jwtSettings);

            return werknemerToken;
        }
    }
}
