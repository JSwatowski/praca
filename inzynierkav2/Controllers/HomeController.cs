using inzynierka.Models;
using inzynierkav2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pracainz.Data;
using pracainz.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace inzynierka.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
             _context = context;

        }

        public IActionResult Index()
        {
            List<Movie> listofMovies = new List<Movie>();
            var movies = _context.Movies.ToList();
            listofMovies = movies;

            listofMovies.Sort((x, y) => y.Premier.CompareTo(x.Premier));
            ViewBag.movieList = listofMovies.GetRange(0, 6);


            List<Movie> listofMoviesRank = new List<Movie>();
            var movieRank = _context.Movies
               //   .Include(c => c.Nibyuser)
               .Include(p => p.Ratings).ToList();
            listofMoviesRank = movieRank;
            listofMoviesRank.Sort((x, y) => y.OverallRating.CompareTo(x.OverallRating));
            ViewBag.movieListRank = listofMoviesRank.GetRange(0, 6);

            return View();
        }

        public IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
