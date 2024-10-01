using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Werf
    {
        public Werf()
        {
            Werknemers = new HashSet<Werknemer>();
        }

        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Naam { get; set; }
        [Required]
        public DateTime StartDatum { get; set; }
        [Required]
        public DateTime EindDatum { get; set; }
        [Required]
        public Enums.Status Status { get; set; }
        public virtual ICollection<Werknemer> Werknemers { get; set; }
    }
}
