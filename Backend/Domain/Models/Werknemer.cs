using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Werknemer
    {
        public Werknemer()
        {
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Voornaam { get; set; }
        [Required]
        [MaxLength(200)]
        public string Achternaam { get; set; }
        public string? Emailadres { get; set; }
        public string? Wachtwoord { get; set; }
        [Required]
        public int WerfId { get; set; }
        [Required]
        public RolType RolId { get; set; }
        public virtual Werf Werf { get; set; }
        public virtual Rol Rol { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
