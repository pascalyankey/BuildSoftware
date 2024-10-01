using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Werknemers = new HashSet<Werknemer>();
        }

        public RolType RolId { get; set; }
        public string RolBeschrijving { get; set; }

        public virtual ICollection<Werknemer> Werknemers { get; set; }
    }
}
