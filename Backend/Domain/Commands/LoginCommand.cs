using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands
{
    public record LoginCommand(string Emailadres, string Wachtwoord) : IRequest<WerknemerToken>;

}
