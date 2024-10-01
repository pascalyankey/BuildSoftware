using Domain.Enums;

namespace API.Dto
{
    public class WerfDto
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public Status Status { get; set; }
        public IEnumerable<WerknemerDto>? Werknemers { get; set; } = new List<WerknemerDto>();
    }
}
