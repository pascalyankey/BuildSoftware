using Domain.Enums;
using Domain.Models;

namespace API.Dto
{
    public class WithInsertWerknemerDto
    {
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string? Emailadres { get; set; }
        public string? Wachtwoord { get; set; }
        public RolType RolId { get; set; }
    }
}
