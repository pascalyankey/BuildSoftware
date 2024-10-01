using Domain.Enums;

namespace API.Dto
{
    public class InsertRolCommandDto
    {
        public RolType RolId { get; set; }
        public string RolBeschrijving { get; set; }
    }
}
