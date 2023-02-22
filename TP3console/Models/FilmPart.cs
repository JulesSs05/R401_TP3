using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3console.Models.EntityFramework
{
    public partial class Film
    {
        public override string? ToString()
        {
            return "id : " + this.Id +
            "\nnom : " + this.Nom +
            "\ndescription : " + this.Description +
            "\ncategorie : " + this.Categorie;
        }
    }
}
