using System;
using System.Collections.Generic;

namespace TP3console.Models.EntityFramework
{
    public partial class Avi
    {
        public int Film { get; set; }
        public int Utilisateur { get; set; }
        public string? Avis { get; set; }
        public decimal Note { get; set; }

        public virtual Film FilmNavigation { get; set; } = null!;
        public virtual Utilisateur UtilisateurNavigation { get; set; } = null!;
    }
}
