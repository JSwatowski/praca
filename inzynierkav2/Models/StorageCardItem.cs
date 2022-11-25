using pracainz.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inzynierka.Models
{
    public class StorageCardItem
    {
        [Key]
        public int Id { get; set; }
        public Movie Movie { get; set; }


        public string StorageCardId { get; set; }
    }
}
