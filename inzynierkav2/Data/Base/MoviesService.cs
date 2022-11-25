using inzynierka.Models;
using Microsoft.EntityFrameworkCore;
using pracainz.Data;
using pracainz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzynierka.Data.Base
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                ImageURL = data.ImageURL,
              //  NibyuserId = data.NibyuserId,
                Premier = data.Premier,
                MovieTypeId = data.MovieTypeId
            };
            await _context.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            foreach(var categoryId in data.CategoryIds)
            {
                var newCategoryMovie = new Movie_Category()
                {
                    MovieId = newMovie.Id,
                    CategoryId = categoryId
                };
                await _context.Movie_Categories.AddAsync(newCategoryMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
             //   .Include(c => c.Nibyuser)
                .Include(p => p.MovieType)
                .Include(z => z.Movie_Categories).ThenInclude(a => a.Category)
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }

        public async Task<NewMovieDropDownsVM> GetNewMovieDropDownsValues()
        {
            var response = new NewMovieDropDownsVM()
            {
                Categories = await _context.Categories.OrderBy(n => n.Name).ToListAsync(),
               // Nibyusers = await _context.Nibyusers.OrderBy(n => n.Name).ToListAsync(),
                MovieTypes = await _context.MovieTypes.OrderBy(n => n.Name).ToListAsync(),
            };
            return response;
        }

    }
}
