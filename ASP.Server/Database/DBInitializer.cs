using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASP.Server.Database
{
    public class DbInitializer
    {
        public static void Initialize(LibraryDbContext bookDbContext)
        {

            if (bookDbContext.Books.Any())
                return;

            Genre Romance, Thriller;
            bookDbContext.Genre.AddRange(
                Romance = new Genre() { Nom = "Romance" },
                Thriller = new Genre() { Nom = "Thriller" }
            );
            bookDbContext.SaveChanges();

            // Une fois les moèles complété Vous pouvez faire directement
            // new Book() { Author = "xxx", Name = "yyy", Price = n.nnf, Content = "ccc", Genres = new() { Romance, Thriller } }
            bookDbContext.Books.AddRange(
                new Book() { Autheur = "xxx", Nom = "yyy", Prix = 0.0, Contenu = "ccc", Genres = new() { Romance, Thriller } }, 
                new Book() { Autheur = "xxx", Nom = "yyy", Prix = 0.0, Contenu = "ccc", Genres = new() { Romance, Thriller } },
                new Book() { Autheur = "xxx", Nom = "yyy", Prix = 0.0, Contenu = "ccc", Genres = new() { Romance, Thriller } },
                new Book() { Autheur = "xxx", Nom = "yyy", Prix = 0.0, Contenu = "ccc", Genres = new() { Romance, Thriller } }
            );
            // Vous pouvez initialiser la BDD ici

            bookDbContext.SaveChanges();
        }
    }
}