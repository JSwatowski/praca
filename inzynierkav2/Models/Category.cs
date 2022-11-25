using inzynierka.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pracainz.Models
{
    public class Category : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        public List<Movie_Category> Movie_Categories { get; set; }

    }
}
