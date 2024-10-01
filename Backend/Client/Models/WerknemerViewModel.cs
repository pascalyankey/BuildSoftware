using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public class WerknemerViewModel
    {
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string? Emailadres { get; set; }
        public string? Wachtwoord { get; set; }
        public int WerfId { get; set; }
        public int RolId { get; set; }
    }
}
