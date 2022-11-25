using pracainz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzynierka.Models
{
    public class NewMovieDropDownsVM
    {
        public NewMovieDropDownsVM()
        {
            Categories = new List<Category>();
         //   Nibyusers = new List<Nibyuser>();
            MovieTypes = new List<MovieType>();

        }
        public List<Category> Categories { get; set; }
     //   public List<Nibyuser> Nibyusers { get; set; }
        public List <MovieType> MovieTypes { get; set; }
    }
}
