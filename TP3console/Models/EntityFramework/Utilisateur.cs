using System;
using System.Collections.Generic;

namespace TP3console.Models.EntityFramework
{
    public partial class Utilisateur
    {
        public Utilisateur()
        {
            Avis = new HashSet<Avi>();
        }

        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Pwd { get; set; } = null!;

        public virtual ICollection<Avi> Avis { get; set; }
    }
}
