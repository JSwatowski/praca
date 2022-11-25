using inzynierka.Data.Base;
using inzynierka.Models.Comments;
using inzynierkav2.Models;
using pracainz.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace pracainz.Models
{
    public class Movie : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Premier { get; set; }
        public string ImageURL { get; set; }



        public List<Movie_Category> Movie_Categories { get; set; }


        // public Nibyuser Nibyuser { get; set; }
        public int MovieTypeId { get; set; }
        [ForeignKey("MovieTypeId")]
        public MovieType MovieType { get; set; }

        public List<Comment> Comments { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }

        [NotMapped]
        public decimal OverallRating
        {
            get
            {
                if(Ratings.Count > 0)
                {
                    return (Ratings.Average(x => x.Rank));
                }
                return (0);
            }
        }
    }
}
