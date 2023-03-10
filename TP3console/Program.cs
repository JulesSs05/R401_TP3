using Microsoft.EntityFrameworkCore;
using TP3console.Models.EntityFramework;
using System.Linq;
using System;

namespace TP3console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //using (var ctx = new FilmsDBContext())
            //{
            //    //Requête SELECT
            //    Film titanic = ctx.Films.First(f => f.Nom.Contains("Titanic"));

            //    //Modification de l'entité (dans le contexte seulement)
            //    titanic.Description = "Un bateau échoué. Date : " + DateTime.Now;

            //    //Sauvegarde du contexte => Application de la modification dans la BD
            //    int nbchanges = ctx.SaveChanges();

            //    Console.WriteLine("Nombre d'enregistrements modifiés ou ajoutés : " + nbchanges);
            //}
            //Console.ReadKey();

            //---------------------------------------------------------------------------------------------

            //using (var ctx = new FilmsDBContext())
            //{
            //    //ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            //    //Requête SELECT
            //    Film titanic = ctx.Films.AsNoTracking().First(f => f.Nom.Contains("Titanic"));

            //    //Modification de l'entité (dans le contexte seulement)
            //    titanic.Description = "Un bateau échoué. Date : " + DateTime.Now;

            //    //Sauvegarde du contexte => Application de la modification dans la BD
            //    int nbchanges = ctx.SaveChanges();

            //    Console.WriteLine("Nombre d'enregistrements modifiés ou ajoutés : " + nbchanges);
            //}
            //Console.ReadKey();

            //---------------------------------------------------------------------------------------------

            //using (var ctx = new FilmsDBContext())
            //{
            //    //Chargement de la catégorie Action
            //    Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
            //    Console.WriteLine("Categorie : " + categorieAction.Nom);
            //    Console.WriteLine("Films : ");
            //    //Chargement des films de la catégorie Action.
            //    foreach (var film in ctx.Films.Where(f => f.CategorieNavigation.Nom ==categorieAction.Nom).ToList())
            //    {
            //        Console.WriteLine(film.Nom);
            //    }
            //}

            //---------------------------------------------------------------------------------------------

            //using (var ctx = new FilmsDBContext())
            //{
            //    Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
            //    Console.WriteLine("Categorie : " + categorieAction.Nom);
            //    //Chargement des films dans categorieAction
            //    ctx.Entry(categorieAction).Collection(c => c.Films).Load();
            //    Console.WriteLine("Films : ");
            //    foreach (var film in categorieAction.Films)
            //    {
            //        Console.WriteLine(film.Nom);
            //    }
            //}

            //---------------------------------------------------------------------------------------------

            //using (var ctx = new FilmsDBContext())
            //{
            //    //Chargement de la catégorie Action et des films de cette catégorie
            //    Categorie categorieAction = ctx.Categories
            //    .Include(c => c.Films)
            //    .First(c => c.Nom == "Action");
            //    Console.WriteLine("Categorie : " + categorieAction.Nom);
            //    Console.WriteLine("Films : ");
            //    foreach (var film in categorieAction.Films)
            //    {
            //        Console.WriteLine(film.Nom);
            //    }
            //}

            //---------------------------------------------------------------------------------------------

            //using (var ctx = new FilmsDBContext())
            //{
            //    //Chargement de la catégorie Action, des films de cette catégorie et des avis
            //    Categorie categorieAction = ctx.Categories
            //    .Include(c => c.Films)
            //    .ThenInclude(f => f.Avis)
            //    .First(c => c.Nom == "Action");
            //}

            //---------------------------------------------------------------------------------------------

            /*using (var ctx = new FilmsDBContext())
            {
                //Chargement de la catégorie Action
                Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
                Console.WriteLine("Categorie : " + categorieAction.Nom);
                Console.WriteLine("Films : ");
                //Chargement des films de la catégorie Action.
                foreach (var film in categorieAction.Films) // lazy loading initiated
                {
                    Console.WriteLine(film.Nom);
                }
            }*/

            //---------------------------------------------------------------------------------------------

            GetNoteMaxUser();
            Console.ReadKey();
        }
        public static void Exo2Q1()
        {
            var ctx = new FilmsDBContext();
            foreach (var film in ctx.Films)
            {
                Console.WriteLine(film.ToString());
            }
        }

        //Autre possibilité :
        public static void Exo2Q1Bis()
        {
            var ctx = new FilmsDBContext();
            //Pour que cela marche, il faut que la requête envoie les mêmes noms de colonnes que les classes c#.
            var films = ctx.Films.FromSqlRaw("SELECT * FROM film");
            foreach (var film in films)
            {
                Console.WriteLine(film.ToString());
            }
        }

        public static void GetEmail()
        {
            var ctx = new FilmsDBContext();
            foreach (var utilisateur in ctx.Utilisateurs)
            {
                Console.WriteLine(utilisateur.Email);
            }
        }
        public static void GetLoginAsc()
        {
            var ctx = new FilmsDBContext();
            foreach (var utilisateur in ctx.Utilisateurs.OrderBy(c => c.Login))
            {
                Console.WriteLine(utilisateur.Login);
            }
        }
        public static void GetFilmActionCat()
        {
            var ctx = new FilmsDBContext();
            //Chargement de la catégorie Action
            Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
            //Chargement des films de la catégorie Action.
            foreach (var film in ctx.Films.Where(f => f.CategorieNavigation.Nom == categorieAction.Nom).ToList())
            {
                Console.WriteLine(film.Nom + "=> Id : " + film.Id);
            }
        }
        public static void GetCategoryCount()
        {
            var ctx = new FilmsDBContext();
            Console.WriteLine(ctx.Categories.Count());
        }
        public static void GetNoteMin()
        {
            var ctx = new FilmsDBContext();
            Console.WriteLine(Math.Round(ctx.Avis.Min(a => a.Note), 2));
        }
        public static void GetFilmBeginLe()
        {
            var ctx = new FilmsDBContext();
            foreach (var film in ctx.Films.Where(f => f.Nom.ToUpper().StartsWith("lE".ToUpper())))
            {
                Console.WriteLine(film.Nom);
            }
        }
        public static void GetNoteAvgPulpFiction()
        {
            var ctx = new FilmsDBContext();
            Decimal average = ctx.Avis.Where(f => f.FilmNavigation.Nom.ToUpper() == "Pulp Fiction".ToUpper()).Average(x => x.Note);
            Console.WriteLine(Math.Round(average,2));
        }
        public static void GetNoteMaxUser()
        {
            var ctx = new FilmsDBContext();
            Decimal noteMax = ctx.Avis.Max(c => c.Note);

            foreach (var avis in ctx.Avis.Where(a => a.Note == noteMax))
            {
                foreach (var user in ctx.Utilisateurs.Where(u => u.Id == avis.Utilisateur))
                {
                    Console.WriteLine(user.Login);
                }
            }
        }
    }
}