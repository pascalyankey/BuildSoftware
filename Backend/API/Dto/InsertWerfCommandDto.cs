using Domain.Enums;

namespace API.Dto
{
    public class InsertWerfCommandDto
    {
        public string Naam { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public Status Status { get; set; }
        public List<WithInsertWerknemerDto> Werknemers { get; set; } = null!;
    }
}
