using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ASP.Server.Data
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        // Mettez ici les propriété de votre livre: Nom, Autheur, Prix, Contenu et Genres associés
        public String Nom { get; set; }
        public String Autheur { get; set; }
        public Double Prix { get; set; }
        public String Contenu { get; set; }
        // N'oublier pas qu'un livre peut avoir plusieur genres
        public List<Genre> Genres { get; set; }
        
    }

    public class Books
    {
        public Book book { init; private get; }

        public string Name => book.Nom;
        public double Price => book.Prix;
        public List<Genre> Genre => book.Genres;
    }
}
