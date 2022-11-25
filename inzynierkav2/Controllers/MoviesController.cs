using inzynierka.Data.Base;
using inzynierka.Models;
using inzynierka.Models.Comments;
using inzynierkav2.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using pracainz.Data;
using pracainz.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;



namespace inzynierka.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMoviesService _service;
        private readonly UserManager<WebUser> _userManager;
        public MoviesController(IMoviesService service, AppDbContext context, UserManager<WebUser> userManager)
        {
            _service = service;
            _context = context;
            _userManager = userManager;
        }

        // GET Categories
        public async Task<IActionResult> Index()
        {
            var allMovies = await _service.GetAllAsync();

            ViewBag.CatList = _context.Categories.ToList();

            List<Movie> listofMovies = new List<Movie>();
            var movies = _context.Movies.ToList();
            listofMovies = movies;

            listofMovies.Sort((x, y) => x.Premier.CompareTo(y.Premier));
            //var allMovies = await _service.GetAllAsync();

            //  List<Movie> moviesHighestData = new List<Movie>(listofMovies);
            //   moviesHighestData.Sort((x, y) => x.Premier.CompareTo(y.Premier));



            return View(allMovies);
        }

        public async Task<IActionResult> IndexSeries()
        {
            var allMovies = await _service.GetAllAsync();


            ViewBag.CatList = _context.Categories.ToList();

            List<Movie> listofMovies = new List<Movie>();
            var movies = _context.Movies.ToList();
            listofMovies = movies;

            listofMovies.Sort((x, y) => x.Premier.CompareTo(y.Premier));
            //var allMovies = await _service.GetAllAsync();

            //  List<Movie> moviesHighestData = new List<Movie>(listofMovies);
            //   moviesHighestData.Sort((x, y) => x.Premier.CompareTo(y.Premier));



            return View(allMovies);
        }

        public async Task<IActionResult> IndexRating()
        {
            var allMovies = _context.Movies.Include(c => c.Ratings);




            return View(await allMovies.ToListAsync());
        }

        public async Task<IActionResult> IndexRatingSeries()
        {
            var allMovies = _context.Movies.Include(c => c.Ratings);



            return View(await allMovies.ToListAsync());
        }
        public async Task<IActionResult> ShowNew()
        {
            var allMovies = await _service.GetAllAsync();
            //var allMovies =  _context.Movies.ToList();
            List<Movie> moviesHighestData = new List<Movie>(allMovies);
            moviesHighestData.Sort((x, y) => x.Premier.CompareTo(y.Premier));

            var moviesHighestData2 = moviesHighestData.Take(4);
            return PartialView("_Partofmovies", moviesHighestData2);
        }
        public async Task<IActionResult> Filter(string searchString)
        {
            ViewBag.CatList = _context.Categories.ToList();
            var allMovies = await _service.GetAllAsync();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(n => n.Name.Contains(searchString) ).ToList();
                return View("Index", filteredResult);
            }
 

            return View("Index", allMovies);
        }
        public async Task<IActionResult> FilterData(DateTime date1, DateTime date2)
        {
            ViewBag.CatList = _context.Categories.ToList();
            var allMovies = await _context.Movies.Where(p => p.Premier >= date1).Where(o => o.Premier <= date2).ToListAsync();


            return View("Index", allMovies);
        }

        public async Task<IActionResult> FilterCat(string searchString)
        {
            ViewBag.CatList = _context.Categories.ToList();
            var allMovies = await _service.GetAllAsync();

            List<Movie> listaftercat = new List<Movie>();
            var movieDetails = _context.Movies.Include(p => p.Movie_Categories).ThenInclude(x => x.Category).ToList();
            if (!string.IsNullOrEmpty(searchString))
            {

                foreach (var item in movieDetails)
                {
                    foreach (var item2 in item.Movie_Categories)
                    {
                        if (item2.Category.Name.Contains(searchString))
                        {
                            listaftercat.Add(item);
                        }

                    }
                }

                return View("Index", listaftercat);
            }


            return View("Index", allMovies);
        }

        public async Task<IActionResult> FilterSerie(string searchString)
        {
            ViewBag.CatList = _context.Categories.ToList();
            var allMovies = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(n => n.Name.Contains(searchString)).ToList();
                return View("IndexSeries", filteredResult);
            }


            return View("IndexSeries", allMovies);
        }
        public async Task<IActionResult> FilterDataSerie(DateTime date1, DateTime date2)
        {
            ViewBag.CatList = _context.Categories.ToList();
            var allMovies = await _context.Movies.Where(p => p.Premier >= date1).Where(o => o.Premier <= date2).ToListAsync();


            return View("IndexSeries", allMovies);
        }

        public async Task<IActionResult> FilterCatSerie(string searchString)
        {
            ViewBag.CatList = _context.Categories.ToList();
            var allMovies = await _service.GetAllAsync();

            List<Movie> listaftercat = new List<Movie>();
            var movieDetails = _context.Movies.Include(p => p.Movie_Categories).ThenInclude(x => x.Category).ToList();
            if (!string.IsNullOrEmpty(searchString))
            {

                foreach (var item in movieDetails)
                {
                    foreach (var item2 in item.Movie_Categories)
                    {
                        if (item2.Category.Name.Contains(searchString))
                        {
                            listaftercat.Add(item);
                        }

                    }
                }

                return View("IndexSeries", listaftercat);
            }


            return View("IndexSeries", allMovies);
        }
        //GET: Movies/Details/1

        public async Task<IActionResult> Details(int id)
        {
            /*
            var movieDetail = await _service.GetMovieByIdAsync(id);

            return View(movieDetail);*/

            if (id == null)
            {
                return BadRequest();
            }
            var movieDetail = await _service.GetMovieByIdAsync(id);
            CommentVM vm = new CommentVM();

            if (movieDetail == null)
            {
                return NotFound();
            }

            vm.MovieId = id;
            vm.Title = movieDetail.Name;
            vm.ImageURL = movieDetail.ImageURL;
            vm.MovieType = movieDetail.MovieType.Name;
            vm.Movie_Categories = movieDetail.Movie_Categories;
            vm.Premier = movieDetail.Premier;
            vm.Description = movieDetail.Description;

            var listofRtings = _context.Ratings.Where(d => d.MovieId.Equals(id)).ToList();
            vm.Ratings = listofRtings;
            var overallrate = _context.Ratings.Where(d => d.MovieId.Equals(id)).ToList();

            decimal overrallRatetoSide = 0;

            foreach (var c in overallrate)
            {
                overrallRatetoSide = overrallRatetoSide + c.Rank;
            }
            if (overallrate.Count == 0)
            {
                overrallRatetoSide = 0;
            }
            else
            {
                overrallRatetoSide = overrallRatetoSide / overallrate.Count;
            }
            vm.OverallRating = overrallRatetoSide;
            var listOfComments = _context.Comments.Where(d => d.MovieId.Equals(id)).ToList();
            vm.ListOfComments = listOfComments;



            return View(vm);
        }

        //GET: Movies/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {

            var movieDropDownsData = await _service.GetNewMovieDropDownsValues();


            ViewBag.Categories = new SelectList(movieDropDownsData.Categories, "Id", "Name");
            ViewBag.MovieTypes = new SelectList(movieDropDownsData.MovieTypes, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropDownsData = await _service.GetNewMovieDropDownsValues();

                ViewBag.Categories = new SelectList(movieDropDownsData.Categories, "Id", "Name");
                ViewBag.MovieTypes = new SelectList(movieDropDownsData.MovieTypes, "Id", "Name");
                return View(movie);
            }

            await _service.AddNewMovieAsync(movie);

            return RedirectToAction(nameof(AdminIndex));

        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _service.GetByIdAsync(id);

            if (movie == null) return View("NotFound");
            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _service.GetByIdAsync(id);
            if (movie == null) return View("NotFound");

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(AdminIndex));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminIndex()
        {
            var allMovies = await _service.GetAllAsync();

            return View(allMovies);
        }
        /*
        [HttpPost]
        public async Task<IActionResult> Comment(CommentVM vm)
        {
            var movie = await _service.GetMovieByIdAsync(vm.MovieId);
            if (!ModelState.IsValid)
                return View(movie);

           // var movie = await _service.GetMovieByIdAsync(vm.MovieId);
            if (vm.MainCommentId > 0)
            {
                movie.MainComments = movie.MainComments ?? new List<MainComment>();

                movie.MainComments.Add(new MainComment
                {
                    Message = vm.Message,
                    Created = DateTime.Now,
                });

                await _service.UpdateAsync(vm.MovieId, movie);
            }else
            {
                var comment = new SubComment
                {
                    MainCommentId = vm.MainCommentId,
                    Message = vm.Message,
                    Created = DateTime.Now,
                };
            }
            await _service.SaveChangesAsync();
            return View(movie);
        */
    }
}



