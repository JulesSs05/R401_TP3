using System;
using System.Collections.Generic;

namespace TP3console.Models.EntityFramework
{
    public partial class Categorie
    {
        public Categorie()
        {
            Films = new HashSet<Film>();
        }

        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Film> Films { get; set; }
    }
}
