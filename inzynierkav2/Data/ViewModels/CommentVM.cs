using inzynierka.Models.Comments;
using inzynierkav2.Models;
using pracainz.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace inzynierkav2.Data.ViewModels
{
    public class CommentVM
    {
        public string Title { get; set; }
        public List<Comment> ListOfComments { get; set; }
        public string Body { get; set; }
        public int MovieId { get; set; }
        public int Rating { get; set; }


        public string Description { get; set; }
        public DateTime Premier { get; set; }
        public string ImageURL { get; set; }
        public List<Movie_Category> Movie_Categories { get; set; }
        public string MovieType { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }

        public decimal OverallRating { get; set; }


        public List<Movie> PartListOfMovies { get; set; }

    }
}
