using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using ASP.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ASP.Server.Controllers
{
    public class CreateGenreModel
    {
        [Required]
        [Display(Name = "Nom")]
        public String Name { get; set; }


        public List<int> Books { get; set; }

        public IEnumerable<Book> AllBooks { get; init; }
    }


    public class GenreController : Controller
    {
        private readonly LibraryDbContext libraryDbContext;

        public GenreController(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        // A vous de faire comme BookController.List mais pour les genres !
        public ActionResult<IEnumerable<Genre>> List()
        {
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché
            List<Genre> ListGenres = libraryDbContext.Genre.Include(genre => genre.Books).ToList();
            return View(ListGenres);
        }

    }
}
