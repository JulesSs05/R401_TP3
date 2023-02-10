using System;
using System.Collections.Generic;

namespace TP3console.Models.EntityFramework
{
    public partial class Film
    {
        public Film()
        {
            Avis = new HashSet<Avi>();
        }

        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string? Description { get; set; }
        public int Categorie { get; set; }

        public virtual Categorie CategorieNavigation { get; set; } = null!;
        public virtual ICollection<Avi> Avis { get; set; }
    }
}
