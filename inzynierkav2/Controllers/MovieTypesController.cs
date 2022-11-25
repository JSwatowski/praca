using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pracainz.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzynierka.Controllers
{
    public class MovieTypesController : Controller
    {
        private readonly AppDbContext _context;
        public MovieTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        [Authorize(Roles = "Admin")]
        public async Task <IActionResult>Index()
        {
            var allMovieTypes =await  _context.MovieTypes.ToListAsync();
           
            return View(allMovieTypes);
        }
    }
}
