using Domain.Enums;

namespace API.Dto
{
    public class WerknemerDto
    {
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Emailadres { get; set; }
        public RolType RolId { get; set; }
        public int WerfId { get; set; }
    }
}
