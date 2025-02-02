﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using ASP.Server.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ASP.Server.Controllers
{
    public class CreateBookModel
    {
        [Required]
        [Display(Name = "Nom")]
        public String Name { get; set; }

        // Ajouter ici tous les champ que l'utilisateur devra remplir pour ajouter un livre
        public String Autheur { get; set; }
        public Double Prix { get; set; }
        public String Contenu { get; set; }

        // Liste des genres séléctionné par l'utilisateur
        public List<int> Genres { get; set; }

        // Liste des genres a afficher à l'utilisateur
        public IEnumerable<Genre> AllGenres { get; init;  }
    }

    public class ModifyBookModel
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public String Name { get; set; }

        // Ajouter ici tous les champ que l'utilisateur devra remplir pour ajouter un livre
        public string Autheur { get; set; }
        public double Prix { get; set; }
        public string Contenu { get; set; }

        // Liste des genres séléctionné par l'utilisateur
        public List<int> Genres { get; set; }

        public List<Genre> GenreList { get; set; }

        // Liste des genres a afficher à l'utilisateur
        public IEnumerable<Genre> AllGenres { get; init; }
    }

    public class BookController : Controller
    {
        private readonly LibraryDbContext libraryDbContext;

        public BookController(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        public ActionResult<IEnumerable<Book>> List()
        {
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché
            List<Book> ListBooks = libraryDbContext.Books.Include(book => book.Genres).ToList();
            return View(ListBooks);
        }

        public ActionResult<CreateBookModel> Create(CreateBookModel book)
        {
            // Le IsValid est True uniquement si tous les champs de CreateBookModel marqués Required sont remplis
            if (ModelState.IsValid)
            {
                // Il faut intéroger la base pour récupérer l'ensemble des objets genre qui correspond aux id dans CreateBookModel.Genres
                List<Genre> genres = libraryDbContext.Genre.Where(genre => book.Genres.Contains(genre.Id)).ToList();
                // Completer la création du livre avec toute les information nécéssaire que vous aurez ajoutez, et metter la liste des gener récupéré de la base aussi
                libraryDbContext.Add(new Book() { Nom = book.Name, Autheur = book.Autheur, Prix = book.Prix, Contenu = book.Contenu, Genres = genres });
                libraryDbContext.SaveChanges();
            }

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(new CreateBookModel() { AllGenres = (IEnumerable<Genre>)libraryDbContext.Genre.ToList() } );
        }


        public ActionResult<IEnumerable<Book>> Delete(long id)
        {
            var book = libraryDbContext.Books.Single(book => book.Id == id);
            libraryDbContext.Books.Remove(book);
            libraryDbContext.SaveChanges();
            return RedirectToAction("List", "Book");
        }
    }
}
