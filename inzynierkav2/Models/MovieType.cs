using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pracainz.Models
{
    public class MovieType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List <Movie> Movies { get; set; }
    }
}
