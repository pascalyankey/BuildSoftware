using Domain.Models;

namespace Domain.Models
{
    public class WerknemerToken
    {
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Error { get; set; }
    }
}
