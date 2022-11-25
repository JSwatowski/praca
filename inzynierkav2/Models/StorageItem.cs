using pracainz.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace inzynierka.Models
{
    public class StorageItem
    {
        [Key]
        public int Id { get; set; }
        
        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }

        public int StorageId { get; set; }
        [ForeignKey("StorageId")]
        public virtual Storage Storage { get; set; }

        public string IsWatched { get; set; }
    }
}
