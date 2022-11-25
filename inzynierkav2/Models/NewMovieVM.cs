using inzynierka.Data.Base;
using pracainz.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace pracainz.Models
{
    public class NewMovieVM
    {
        [Display(Description = "Movie name")]
        [Required(ErrorMessage ="Enter value")]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Premier { get; set; }
        public string ImageURL { get; set; }



        public List<int> CategoryIds { get; set; }

        public int NibyuserId { get; set; }
        public int MovieTypeId { get; set; }

    }
}
