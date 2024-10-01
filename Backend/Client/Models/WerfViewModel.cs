using System.ComponentModel.DataAnnotations;
using System;
using Domain.Enums;

namespace Client.Models
{
    public class WerfViewModel
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public Status Status { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string? Emailadres { get; set; }
        public string? Wachtwoord { get; set; }
        public List<WerknemerViewModel> Werknemers { get; set; }
    }
}
