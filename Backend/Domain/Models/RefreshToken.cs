
namespace Domain.Models
{
    public partial class RefreshToken
    {
        public int TokenId { get; set; }
        public int WerknemerId { get; set; }
        public string Token { get; set; }
        public DateTime VervalDatum { get; set; }

        public virtual Werknemer Werknemer { get; set; }
    }
}
