using inzynierkav2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using inzynierka.Models;
using pracainz.Data;

namespace inzynierkav2.Controllers
{
    public class RatingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<WebUser> _userManager;
        public RatingController(AppDbContext context, UserManager<WebUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> SetRating(int movieId, decimal rank)
        {
            var user = _userManager.GetUserId(HttpContext.User);

            var checkIfhasrate = _context.Ratings.Where(x => x.UserId == user);
                checkIfhasrate = checkIfhasrate.Where(y => y.MovieId == movieId);
            //checkIfhasrate = checkIfhasrate.Where(y => y.MovieId == movieId)
            if (checkIfhasrate.Count() == 0)
            {
                Rating rating = new Rating();
                rating.Rank = rank;
                rating.MovieId = movieId;
                rating.UserId = user;

                _context.Ratings.Add(rating);

            }
            else
            {
                foreach (var c in checkIfhasrate)
                {
                    c.Rank = rank;
                    _context.Ratings.Update(c);
                }
            }


            /*
            foreach (var c in checkIfhasrate)
            {
                if (c.UserId == user)
                {
                    if (c.MovieId == movieId)
                    {
                        c.Rank = rank;
                        _context.Ratings.Update(c);

                    }
                    else
                    {
                        Rating rating = new Rating();
                        rating.Rank = rank;
                        rating.MovieId = movieId;
                        rating.UserId = user;

                        _context.Ratings.Add(rating);
                    }
                }
                if (c.UserId != user)
                {
                    Rating rating = new Rating();
                    rating.Rank = rank;
                    rating.MovieId = movieId;
                    rating.UserId = user;

                    _context.Ratings.Add(rating);
                    break;
                }

            }
            */
            await _context.SaveChangesAsync();
            /*
            Rating rating = new Rating();
            rating.Rank = rank;
            rating.MovieId = movieId;
            rating.UserId = user;
             _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            */



            return RedirectToAction("Details", "Movies", new { id = movieId });
        }

        public PartialViewResult RatingsControl(int movieId)
        {

            return PartialView();
        }
    }
}
