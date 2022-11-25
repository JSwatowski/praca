using pracainz.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inzynierkav2.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public Movie Movies { get; set; }

        [Range(0,9)]
        public decimal Rank { get; set; }
    }
}
